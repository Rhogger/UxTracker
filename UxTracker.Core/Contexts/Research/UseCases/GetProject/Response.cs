using Flunt.Notifications;
using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Core.Contexts.Research.UseCases.GetProject;

public class Response : Shared.UseCases.Response
{
    protected Response() { }

    public Response(string message, int statusCode, IEnumerable<Notification>? notifications = null)
    {
        Message = message;
        StatusCode = statusCode;
        Notifications = notifications;
    }

    public Response(string message, ResponseData data)
    {
        Message = message;
        StatusCode = 200;
        Notifications = null;
        Data = data;
    }

    public ResponseData? Data { get; set; }
}

public record ResponseData(Project project);