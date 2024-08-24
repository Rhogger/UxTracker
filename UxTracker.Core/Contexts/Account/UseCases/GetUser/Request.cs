using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.GetUser;

public class Request : IRequest<Response>
{
    public string Id { get; set; } = string.Empty;
}