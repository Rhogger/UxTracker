using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.VerifyReviewer.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.VerifyReviewer;

public class Repository: IRepository
{
    private readonly AppDbContext _context;
    
    public Repository(AppDbContext context) => _context = context;

    public async Task<Reviewer?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await _context
            .Reviewers
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

    public async Task ValidateVerificationCodeAsync(Reviewer user, CancellationToken cancellationToken)
    {
        user.Email.Verification.Verify(user.Email.Verification.Code);
        
        _context
            .Reviewers
            .Update(user);

        await _context.SaveChangesAsync(cancellationToken);
    }
}