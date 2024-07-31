using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;

public class Request: IRequest<Response>
{
    public string Email { get; set; } = string.Empty;
    public string ResetCode { get; set; } = string.Empty;
}