using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.Verify;

public class Request() : IRequest<Response>
{
    public string Email { get; set; } = string.Empty;
    public string VerificationCode { get; set; } = string.Empty;
}