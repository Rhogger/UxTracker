using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.DeleteResearcher;

public class Request : IRequest<Response>
{
    public string Id { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}