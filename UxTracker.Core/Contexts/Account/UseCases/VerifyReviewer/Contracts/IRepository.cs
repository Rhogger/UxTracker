using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.VerifyReviewer.Contracts;

public interface IRepository
{
    Task<Reviewer?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    public void AttachRoles(List<Role> roles);
    Task ValidateVerificationCodeAsync(Reviewer user, CancellationToken cancellationToken);
    Task<bool> AnyProjectAsync(string id, CancellationToken cancellationToken);
}