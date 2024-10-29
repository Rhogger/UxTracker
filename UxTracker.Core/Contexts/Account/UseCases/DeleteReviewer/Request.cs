using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.DeleteReviewer;

public class Request : IRequest<Response>
{
    public string Id { get; set; } = string.Empty;
}