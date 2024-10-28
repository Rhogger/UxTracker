using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer.Contracts;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.AuthenticateReviewer;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Reviewer?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) 
        => await context
            .Reviewers
            .Include(x => x.Roles)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken);

    public async Task<bool> AnyProjectAsync(string id, CancellationToken cancellationToken) =>
        await context
            .Projects
            .AsNoTracking()
            .AnyAsync(x => x.Id.ToString() == id, cancellationToken);

    public async Task<Status> GetStatusAsync(string id, CancellationToken cancellationToken) =>
        await context
            .Projects
            .AsNoTracking()
            .Where(x => x.Id.ToString().Equals(id))
            .Select(x => x.Status)
            .FirstOrDefaultAsync(cancellationToken);
}