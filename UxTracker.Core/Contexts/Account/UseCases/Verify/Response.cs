using Flunt.Notifications;
using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Contexts.Account.UseCases.Verify;

public class Response: Shared.UseCases.Response
{
    protected Response() { }

    public Response(string message, int statusCode, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        StatusCode = statusCode;
        Notifications = notifications;
    }
    
    public Response(string message, Payload data)
    {
        Message = message;
        StatusCode = 200;
        Notifications = null;
        Data = data;
    }
    
    public Payload? Data { get; set; }

}

