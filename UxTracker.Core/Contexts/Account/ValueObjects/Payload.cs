namespace UxTracker.Core.Contexts.Account.ValueObjects;

public record Payload
{
    public string? Token { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
};