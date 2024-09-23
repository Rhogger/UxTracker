using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.CreateReviewer.Contracts;

public interface IService
{
    Task SendVerificationEmailAsync(Reviewer user, CancellationToken cancellationToken);
}