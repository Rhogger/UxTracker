using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.PasswordRecoveryVerify;

public class Repository: IRepository
{
    private readonly AppDbContext _context;
    
    public Repository(AppDbContext context) => _context = context;

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task ValidateResetCodeAsync(User user, CancellationToken cancellationToken)
    {
        user.Password.ResetCode?.Verify(user.Password.ResetCode.Code);

        _context
            .Users
            .Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}