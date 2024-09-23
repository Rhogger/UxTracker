using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.CreateReviewer.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.CreateReviewer;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task<bool> AnyAsync(string email, CancellationToken cancellationToken) =>
        await _context
            .Reviewers
            .AsNoTracking()
            .AnyAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task<Role?> GetRoleByNameAsync(string roleName, CancellationToken cancellationToken) =>
        await _context.Roles.AsNoTracking().FirstOrDefaultAsync(r => r.Name == roleName, cancellationToken);
    
    public void AttachRole(Role role)
    {
        _context.Roles.Attach(role);
    }
    
    public async Task SaveAsync(Reviewer user, CancellationToken cancellationToken)
    {
        await _context.Reviewers.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}