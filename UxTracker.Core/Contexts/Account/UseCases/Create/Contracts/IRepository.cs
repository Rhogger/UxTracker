using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.Create.Contracts;

public interface IRepository
{
    Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
    public Task<Role?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken);
    public Task AddUserRoleAsync(Guid roleId, Guid userId, CancellationToken cancellationToken);

    Task SaveAsync(User user, CancellationToken cancellationToken);
}
