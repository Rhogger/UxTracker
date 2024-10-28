using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.ResendResetCode.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.ResendResetCode;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await context
            .Researchers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task UpdateResetCodeAsync(Researcher user, CancellationToken cancellationToken)
    {
        context
            .Researchers
            .Update(user);

        await context.SaveChangesAsync(cancellationToken);
    }
}