using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeReviewer.Contracts;

public interface IService
{
    Task ResendVerificationCodeAsync(Reviewer user, CancellationToken cancellationToken);
}