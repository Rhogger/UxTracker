using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.UpdatePassword.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.UpdatePassword;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await context
            .Researchers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task UpdatePasswordAsync(Researcher user, string plainTextPassword, CancellationToken cancellationToken)
    {
        user.UpdatePassword(plainTextPassword);

        context
            .Researchers
            .Update(user);

        await context.SaveChangesAsync(cancellationToken);
    }
}