using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendResetCode.Contracts;

public interface IService
{
    Task ResendResetCodeAsync(Researcher user, CancellationToken cancellationToken);
}