using MediatR;

namespace UxTracker.Core.Contexts.Shared.UseCases;

public interface ITransactionalHandler<in TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    Task RollbackAsync();
    Task CommitAsync();
}