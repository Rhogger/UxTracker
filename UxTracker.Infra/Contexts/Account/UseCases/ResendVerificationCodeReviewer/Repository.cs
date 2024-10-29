using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeReviewer.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.ResendVerificationCodeReviewer;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Reviewer?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await context
            .Reviewers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task UpdateVerificationCodeAsync(Reviewer user, CancellationToken cancellationToken)
    {
        context
            .Reviewers
            .Update(user);

        await context.SaveChangesAsync(cancellationToken);
    }
}