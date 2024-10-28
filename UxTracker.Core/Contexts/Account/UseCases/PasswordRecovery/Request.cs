using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery;

public class Request: IRequest<Response>
{
    public string Email { get; init; } = string.Empty;
}