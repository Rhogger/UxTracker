namespace UxTracker.Core.Contexts.Account.ValueObjects;

public record Payload
{
    public string Id { get; set; } = string.Empty;
    public string[]? Roles { get; set; } = []; 
    public string? AccessToken { get; set; } = string.Empty;
    public string? RefreshToken { get; set; } = string.Empty;
};