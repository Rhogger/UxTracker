using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.UpdateAccount.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.UpdateAccount;

public class Repository: IRepository
{
    private readonly AppDbContext _context;
    
    public Repository(AppDbContext context) => _context = context;

    public async Task<User?> GetUserByIdAsync(string id, CancellationToken cancellationToken) 
        => await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);

    public async Task UpdateAccountAsync(User user, CancellationToken cancellationToken)
    {
        _context
            .Users
            .Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}