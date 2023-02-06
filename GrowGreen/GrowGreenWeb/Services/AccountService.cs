using GrowGreenWeb.Models;

namespace GrowGreenWeb.Services;

// basics done by ashlee

public class AccountService
{
    /// <summary>
    /// Keeps track of active user sessions by session ID.
    /// Best to convert this to a DB so we can make this not a singleton :"D
    /// </summary>
    private Dictionary<string, User> Sessions { get; set; } = new();

    private GrowGreenContext _context;
    
    // public AccountService(GrowGreenContext context)
    public AccountService()
    {
        // _context = context;  // change to transient when we make this not a singleton
        _context = new GrowGreenContext();  // comment this out when we make this not a singleton
    }
    
    
    
    public User? GetCurrentUser(string sessionId, AccountType? accountType = null)
    {
        if (Sessions.ContainsKey(sessionId))
        {
            return Sessions[sessionId];
        }
        
        // todo: check account type

        if (accountType == AccountType.Admin)
        {
            return _context.Users.Find(TemporaryConstants.AdminId);
        } else if (accountType == AccountType.Lecturer)
        {
            return _context.Users.Find(TemporaryConstants.LecturerId);
        }
        else if (accountType == AccountType.Learner)
        {
            return _context.Users.Find(TemporaryConstants.LearnerId);
        }

        return null;
    }
    
    /// <summary>
    /// If AccountType is null, attempt to log in as a user of any type.
    /// </summary>
    /// <param name="email"></param>
    /// <param name="password"></param>
    /// <param name="accountType"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public User? Login(string email, string password, AccountType? accountType = null)
    {
        throw new NotImplementedException();
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
}

public enum AccountType
{
    Learner,
    Lecturer,
    Admin
}