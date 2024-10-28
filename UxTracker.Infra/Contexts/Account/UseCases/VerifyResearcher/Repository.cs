using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.VerifyResearcher.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.VerifyResearcher;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await context
            .Researchers
            .Include(x => x.Roles)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);
    
    public void AttachRoles(List<Role?> roles)
    {
        foreach (var role in roles.OfType<Role>())
        {
            context.Roles.Attach(role);
        }
    }

    public async Task ValidateVerificationCodeAsync(Researcher user, CancellationToken cancellationToken)
    {
        user.Email.Verification.Verify(user.Email.Verification.Code);
        
        context
            .Researchers
            .Update(user);

        await context.SaveChangesAsync(cancellationToken);
    }
}