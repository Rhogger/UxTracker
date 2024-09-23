using System.Text.Json.Serialization;
using Flunt.Notifications;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeResearcher;

public class Response: Shared.UseCases.Response
{
    protected Response(){}
    
    public Response(string message, int statusCode, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        StatusCode = statusCode;
        Notifications = notifications;
    }
}