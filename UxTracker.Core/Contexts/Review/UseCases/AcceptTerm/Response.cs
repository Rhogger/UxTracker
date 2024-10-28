using Flunt.Notifications;

namespace UxTracker.Core.Contexts.Review.UseCases.AcceptTerm;

public class Response : Shared.UseCases.Response
{
    protected Response() { }

    public Response(string? message, int statusCode, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        StatusCode = statusCode;
        Notifications = notifications;
    }
}