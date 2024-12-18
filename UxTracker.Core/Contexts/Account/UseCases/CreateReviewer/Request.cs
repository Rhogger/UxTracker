using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.CreateReviewer;

public class Request : IRequest<Response>
{
    public string Email { get; set; } = string.Empty;
    public string Sex { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }
    public string? Country { get; set; } = string.Empty;
    public string? State { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
}