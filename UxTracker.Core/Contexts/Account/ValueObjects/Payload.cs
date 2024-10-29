using System.Text.Json.Serialization;

namespace UxTracker.Core.Contexts.Account.ValueObjects;

public record Payload
{
    public string Id { get; set; } = string.Empty;
    [JsonIgnore]
    public string?[] Roles { get; set; } = Array.Empty<string>(); 
    public string? AccessToken { get; set; } = string.Empty;
    public string? RefreshToken { get; set; } = string.Empty;
};