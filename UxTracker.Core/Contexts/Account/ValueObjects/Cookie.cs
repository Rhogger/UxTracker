namespace UxTracker.Core.Contexts.Account.ValueObjects;

public class Cookie
{
    public string Key { get; set; }

    public string Value { get; set; }

    public DateTime? Expiration { get; set; }

    public Cookie(string key, string value, DateTime? expiration = null)
    {
        Key = key;
        Value = value;
        Expiration = expiration == null ? DateTime.UtcNow.AddHours(1) : null;
    }
}