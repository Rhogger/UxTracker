using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.GetResearcher;

public class Request : IRequest<Response>
{
    public string Id { get; init; } = string.Empty;
}