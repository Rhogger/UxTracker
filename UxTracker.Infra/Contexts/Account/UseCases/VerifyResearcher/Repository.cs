using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.VerifyResearcher.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.VerifyResearcher;

public class Repository: IRepository
{
    private readonly AppDbContext _context;
    
    public Repository(AppDbContext context) => _context = context;

    public async Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context
            .Researchers
            .Include(x => x.Roles)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);
    
    public void AttachRoles(List<Role> roles)
    {
        foreach (var role in roles)
        {
            _context.Roles.Attach(role);
        }
    }

    public async Task ValidateVerificationCodeAsync(Researcher user, CancellationToken cancellationToken)
    {
        user.Email.Verification.Verify(user.Email.Verification.Code);
        
        _context
            .Researchers
            .Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}