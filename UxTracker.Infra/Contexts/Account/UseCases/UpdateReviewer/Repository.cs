using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.UpdateReviewer.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.UpdateReviewer;

public class Repository: IRepository
{
    private readonly AppDbContext _context;
    
    public Repository(AppDbContext context) => _context = context;

    public async Task<Reviewer?> GetUserByIdAsync(string id, CancellationToken cancellationToken) 
        => await _context
            .Reviewers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == true, cancellationToken);

    public async Task UpdateAccountAsync(Reviewer user, CancellationToken cancellationToken)
    {
        _context
            .Reviewers
            .Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}