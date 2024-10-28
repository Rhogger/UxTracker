using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.GetReviewer;

public class Request : IRequest<Response>
{
    public string Id { get; init; } = string.Empty;
}