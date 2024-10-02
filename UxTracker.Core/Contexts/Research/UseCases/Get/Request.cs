using MediatR;

namespace UxTracker.Core.Contexts.Research.UseCases.Get;

public class Request : IRequest<Response>
{
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
}