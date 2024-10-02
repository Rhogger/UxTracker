using UxTracker.Core.Contexts.Review.Entities;

namespace UxTracker.Core.Contexts.Review.UseCases.AcceptTerm.Contracts;

public interface IRepository
{
    Task AcceptTermAsync(UserAcceptedTcle tcle, CancellationToken cancellationToken);
}