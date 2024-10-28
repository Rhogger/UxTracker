using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.CreateReviewer.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.CreateReviewer;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken) =>
        await context
            .Reviewers
            .AsNoTracking()
            .AnyAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task<Role?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken) =>
        await context
            .Roles
            .FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
    
    public void AttachRole(Role? role)
    {
        if (role != null) context.Roles.Attach(role);
    }
    
    public async Task SaveAsync(Reviewer user, CancellationToken cancellationToken)
    {
        await context.Reviewers.AddAsync(user, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}