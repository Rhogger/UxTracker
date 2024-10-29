using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer.Contracts;

public interface IRepository
{
    Task<Reviewer?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    Task<bool> AnyProjectAsync(string id, CancellationToken cancellationToken);
    Task<Status> GetStatusAsync(string id, CancellationToken cancellationToken);
}