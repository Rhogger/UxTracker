using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeResearcher;

public class Request() : IRequest<Response>
{
    public Request(string email) : this()
    {
        Email = email;
    }
    public string Email { get; set; } = string.Empty;
}