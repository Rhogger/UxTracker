using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.VerifyReviewer.Contracts;

public interface IService
{
    string? GenerateAccessToken(Reviewer user, CancellationToken cancellationToken);
    string GenerateRefreshToken(Reviewer user, CancellationToken cancellationToken);
}