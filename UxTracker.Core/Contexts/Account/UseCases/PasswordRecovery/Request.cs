using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery;

public class Request: IRequest<Response>
{
    public string Email { get; set; } = string.Empty;
}