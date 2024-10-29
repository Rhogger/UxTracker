using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer;

public class Request : IRequest<Response>
{
    public string Email { get; set; } = string.Empty;
    public string ResearchCode { get; set; } = string.Empty;
}
