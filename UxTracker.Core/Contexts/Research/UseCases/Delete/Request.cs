using MediatR;

namespace UxTracker.Core.Contexts.Research.UseCases.Delete;

public class Request : IRequest<Response>
{
    public string UserId { get; init; } = string.Empty;
    public string ProjectId { get; init; } = string.Empty;
}