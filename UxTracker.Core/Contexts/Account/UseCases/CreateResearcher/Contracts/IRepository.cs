using UxTracker.Core.Contexts.Account.Entities;

namespace UxTracker.Core.Contexts.Account.UseCases.CreateResearcher.Contracts;

public interface IRepository
{
    Task<bool> AnyAsync(string email, CancellationToken cancellationToken);
    public Task<Role?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken);
    public void AttachRole(Role role);
    Task SaveAsync(Researcher user, CancellationToken cancellationToken);
}