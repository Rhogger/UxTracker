using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.AuthenticateResearcher.Contracts;

public interface IService
{
    string? GenerateAccessToken(Researcher user, CancellationToken cancellationToken);
    string GenerateRefreshToken(Researcher user, CancellationToken cancellationToken);
}