using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.CreateResearcher;

public class Request : IRequest<Response>
{
    public string? Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}