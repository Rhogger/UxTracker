using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.UpdateResearcher.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.UpdateResearcher;

public class Repository: IRepository
{
    private readonly AppDbContext _context;
    
    public Repository(AppDbContext context) => _context = context;

    public async Task<Researcher?> GetUserByIdAsync(string id, CancellationToken cancellationToken) 
        => await _context
            .Researchers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == true, cancellationToken);

    public async Task UpdateAccountAsync(Researcher user, CancellationToken cancellationToken)
    {
        _context
            .Researchers
            .Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}