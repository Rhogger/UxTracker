using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer;

using Flunt.Notifications;

public class Response : Shared.UseCases.Response
{
    protected Response() { }

    public Response(string message, int statusCode, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        StatusCode = statusCode;
        Notifications = notifications;
    }
    
    public Response(string message, int statusCode, ResponseData? data)
    {
        Message = message;
        StatusCode = statusCode;
        Notifications = null;
        Data = data;
    }
    
    public Response(string message, ResponseData? data)
    {
        Message = message;
        StatusCode = 200;
        Notifications = null;
        Data = data;
    }

    public ResponseData? Data { get; set; }
}

public record ResponseData(string? researchCode = null, Payload? payload = null);