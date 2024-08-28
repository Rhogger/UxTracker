using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.Verify.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.Verify;

public class Repository: IRepository
{
    private readonly AppDbContext _context;
    
    public Repository(AppDbContext context) => _context = context;

    public async Task<User?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task ValidateVerificationCodeAsync(User user, CancellationToken cancellationToken)
    {
        user.Email.Verification.Verify(user.Email.Verification.Code);
        
        _context
            .Users
            .Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}