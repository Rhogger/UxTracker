using Flunt.Notifications;

namespace UxTracker.Core.Contexts.Shared.UseCases;

public class Response
{
    public string? Message { get; set; } = string.Empty;
    public int StatusCode { get; set; } = 200;
    public bool IsSuccess => StatusCode is >= 200 and <= 299;
    public IEnumerable<Notification>? Notifications { get; set; }
}
