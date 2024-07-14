using Flunt.Notifications;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCode;

public class Response: Shared.UseCases.Response
{
    protected Response(){}
    
    public Response(string message, int statusCode, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        StatusCode = statusCode;
        Notifications = notifications;
    }

    public Response(string message, ResponseData data)
    {
        Message = message;
        Notifications = null;
        Data = data;
    }
 
    public ResponseData? Data { get; set; }
}

public record ResponseData(string Code);