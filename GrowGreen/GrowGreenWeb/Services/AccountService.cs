using System.Security.Cryptography;
using System.Text;
using GrowGreenWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace GrowGreenWeb.Services;

// basics done by ashlee

public class AccountService
{
    /// <summary>
    /// Keeps track of active user sessions by session ID.
    /// Even though we save inside the DB, cache logins so it's a bit faster
    /// </summary>
    private static List<UserSession> CachedSessions { get; set; } = new();

    private GrowGreenContext _context;

    // public AccountService(GrowGreenContext context)
    public AccountService(GrowGreenContext context)
    {
        _context = context;  // change to transient when we make this not a singleton
        // _context = new GrowGreenContext(); // comment this out when we make this not a singleton
    }

    public bool LoginSession(HttpContext httpContext, string email, string password, bool rememberMe = false)
    {
        User? user = Login(email, password);

        if (user is null)
            return false;

        // create new session, adding to both cache and db
        UserSession session = new()
        {
            SessionId = Guid.NewGuid().ToString(),
            User = user
        };

        _context.Add(session);
        _context.SaveChanges();

        CachedSessions.Add(session);

        // update the HttpContext with the SessionID
        // httpContext.Session.SetString("SessionId", session.SessionId);
        CookieOptions cookieOptions = new CookieOptions
        {
            Expires = rememberMe
                ? Constants.AccountRememberMe
                : DateTime.Now.AddMinutes(Constants.AccountNoRememberMeMinsExpiry),
            Path = "/"
        };
        httpContext.Response.Cookies.Append("UserSessionId", session.SessionId, cookieOptions);

        return true;
    }

    public void LogoutSession(HttpContext httpContext)
    {
        foreach (var cookie in httpContext.Request.Cookies.Keys)
        {
            httpContext.Response.Cookies.Delete(cookie);
        }
    }

    public User? GetCurrentUser(HttpContext context, AccountType? accountType = null)
    {
        // string? sessionId = context.Session.GetString("SessionId");
        string? sessionId = context.Request.Cookies["UserSessionId"];
        if (sessionId != null)
        {
            return GetCurrentUser(sessionId, accountType);
        }

        return null;
    }
    private User? GetCurrentUser(string sessionId, AccountType? accountType = null)
    {
        UserSession? session = CachedSessions.SingleOrDefault(s => s.SessionId == sessionId);
        if (session != null)
        {
            return session.User;
        }

        // if not in cache, check db
        session = _context.UserSessions
            .Include(s => s.User)
            .SingleOrDefault(s => s.SessionId == sessionId);

        if (session == null)
        {
            return null;
        }

        // check account type
        switch (accountType)
        {
            case AccountType.Admin:
                return session.User.IsAdmin ? session.User : null;
            case AccountType.Lecturer:
                return session.User.IsLecturer ? session.User : null;
            case AccountType.Learner:
                return session.User.IsLearner ? session.User : null;
            default:
                return session.User;
        }
    }

    /// <summary>
    /// If AccountType is null, attempt to log in as a user of any type.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="accountType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    private User? Login(string email, string password, AccountType? accountType = null)
    {
        // get user account
        User? user = _context.Users
            .SingleOrDefault(u => u.Email == email && u.Password == HashPassword(password));

        if (user is null)
            return null;

        switch (accountType)
        {
            case AccountType.Admin:
                return user.IsAdmin ? user : null;
            case AccountType.Lecturer:
                return user.IsLecturer ? user : null;
            case AccountType.Learner:
                return user.IsLearner ? user : null;
            default:
                return user;
        }
    }

    /// <summary>
    /// TODO: Add fields such as address
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="accountType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public User? Register(string email, string password, AccountType accountType)
    {
        throw new NotImplementedException();
    }

    public bool ChangePassword(string email, string passwordPlaintext)
    {
        // get user account
        User? user = _context.Users.SingleOrDefault(u => u.Email == email);
        if (user == null)
            return false;

        // update pw
        user.Password = HashPassword(passwordPlaintext);
        _context.SaveChanges();

        return true;
    }

    private string HashPassword(string plaintext)
    {
        using var sha256 = SHA256.Create();
        byte[] inputBytes = Encoding.UTF8.GetBytes(plaintext);
        byte[] hashBytes = sha256.ComputeHash(inputBytes);
        return Convert.ToHexString(hashBytes);
    }
}

public enum AccountType
{
    Learner,
    Lecturer,
    Admin
}