using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdateReviewer;

public class Request: IRequest<Response>
{
    public string? Id { get; set; } = string.Empty;
    public string? Country { get; set; } = string.Empty;
    public string? State { get; set; } = string.Empty;
    public string? City { get; set; } = string.Empty;
}