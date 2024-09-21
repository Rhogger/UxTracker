using MediatR;

namespace UxTracker.Core.Contexts.Research.UseCases.Delete;

public class Request : IRequest<Response>
{
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
}