using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeReviewer.Contracts;

public interface IRepository
{
    Task<Reviewer?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);

    Task UpdateVerificationCodeAsync(Reviewer user, CancellationToken cancellationToken);
}