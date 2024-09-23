using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.AuthenticateReviewer;

public class Repository: IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;
    public async Task<Reviewer?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) 
        => await _context
            .Reviewers
            .Include(x => x.Roles)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken);

    public async Task<bool> AnyProjectAsync(string id, CancellationToken cancellationToken) =>
        await _context
            .Projects
            .AsNoTracking()
            .AnyAsync(x => x.Id.ToString() == id,cancellationToken: cancellationToken);
}