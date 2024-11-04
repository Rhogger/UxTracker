namespace UxTracker.Core;

public static class Configuration
{
    public static SecretsConfiguration Secrets { get; set; } = new();
    public static EmailConfiguration Email { get; set; } = new();
    public static SendGridConfiguration SendGrid { get; set; } = new();
    public static DatabaseConfiguration Database { get; set; } = new();
    public static ApplicationUrlConfiguration ApplicationUrl { get; set; } = new();
    public static CorsConfiguration Cors { get; set; } = new();
    public static CookieConfiguration Cookie { get; set; } = new();
    public static ConsentTermConfiguration ConsentTerm { get; set; } = new();

    public class SecretsConfiguration
    {
        public string JwtPrivateKey { get; set; } = string.Empty;
        public string PasswordSaltKey { get; set; } = string.Empty;
    }

    public class EmailConfiguration
    {
        public string DefaultFromEmail { get; set; } = string.Empty;
        public string DefaultFromName { get; set; } = string.Empty;
    }

    public class SendGridConfiguration
    {
        public string ApiKey { get; set; } = string.Empty;
    }

    public class DatabaseConfiguration
    {
        public string ConnectionString { get; set; } = string.Empty;
    }

    public class ApplicationUrlConfiguration
    {
        public string BackendUrl { get; set; } = "https://uxtracker.duckdns.org/";
        public string FrontendUrl { get; set; } = "https://uxtracker.duckdns.org/";
    }
    
    public class CorsConfiguration
    {
        public string CorsPolicyName { get; set; } = string.Empty;
    }
    
    public class CookieConfiguration
    {
        public string? AccessTokenCookieName { get; set; } = "AccessToken";
        public string? RefreshTokenCookieName { get; set; } = "RefreshToken";
    }
    
    public class ConsentTermConfiguration
    {
        public string Url { get; set; } = Path.Combine(Directory.GetCurrentDirectory(), "media", "terms");
        public string Folder { get; set; } = "terms/";
        public long MaxSize { get; set; } = 2 * 1024 * 1024;
    }
}