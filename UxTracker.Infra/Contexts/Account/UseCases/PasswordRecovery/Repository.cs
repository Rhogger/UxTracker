using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.PasswordRecovery;

public class Repository: IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;
    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email, cancellationToken: cancellationToken);

    public async Task UpdateResetCodeAsync(User user, CancellationToken cancellationToken)
    {
        _context
            .Users
            .Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}