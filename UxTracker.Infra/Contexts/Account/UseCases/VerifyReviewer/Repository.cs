using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.VerifyReviewer.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.VerifyReviewer;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Reviewer?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await context
            .Reviewers
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

    public async Task ValidateVerificationCodeAsync(Reviewer user, CancellationToken cancellationToken)
    {
        user.Email.Verification.Verify(user.Email.Verification.Code);
        
        context
            .Reviewers
            .Update(user);

        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task<bool> AnyProjectAsync(string id, CancellationToken cancellationToken) =>
        await context
            .Projects
            .AsNoTracking()
            .AnyAsync(x => x.Id.ToString() == id,cancellationToken: cancellationToken);
}