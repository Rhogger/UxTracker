using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendResetCode.Contracts;

public interface IRepository
{
    Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

    Task UpdateResetCodeAsync(Researcher user, CancellationToken cancellationToken);
}