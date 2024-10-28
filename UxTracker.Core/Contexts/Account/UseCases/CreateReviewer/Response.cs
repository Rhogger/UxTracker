using Flunt.Notifications;

namespace UxTracker.Core.Contexts.Account.UseCases.CreateReviewer;

public class Response : Shared.UseCases.Response
{
    public Response(string? message, int statusCode, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        StatusCode = statusCode;
        Notifications = notifications;
    }
}