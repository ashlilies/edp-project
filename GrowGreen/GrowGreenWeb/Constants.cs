namespace GrowGreenWeb;

public static class Constants
{
    public static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".gif" };

    public static readonly string[] AllowedVideoExtensions = { ".mp4", ".ogg" };

    public static readonly string UnauthorizedRedirect = "/Account/Login";

    public static readonly DateTimeOffset AccountRememberMe 
        = new DateTimeOffset(2038, 1, 1, 0, 0, 0, TimeSpan.FromHours(0));

    public static readonly int AccountNoRememberMeMinsExpiry = 60;
}