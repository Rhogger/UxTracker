using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.Verify.Contracts;

public interface IRepository
{
    Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken);
    public Task<Role?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken);
    Task ValidateVerificationCodeAsync(User user, CancellationToken cancellationToken);
}