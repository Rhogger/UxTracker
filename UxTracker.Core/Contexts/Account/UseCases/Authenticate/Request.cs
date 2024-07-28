using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.Authenticate;

public class Request : IRequest<Response>
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
