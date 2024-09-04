using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.Verify.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    public void AttachRoles(List<Role> roles);
    Task ValidateVerificationCodeAsync(User user, CancellationToken cancellationToken);
}