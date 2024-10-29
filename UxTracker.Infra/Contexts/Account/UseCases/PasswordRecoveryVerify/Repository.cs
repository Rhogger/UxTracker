using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.PasswordRecoveryVerify;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken)
        => await context
            .Researchers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken: cancellationToken);

    public async Task ValidateResetCodeAsync(Researcher user, CancellationToken cancellationToken)
    {
        user.Password?.ResetCode?.Verify(user.Password.ResetCode.Code);

        context
            .Researchers
            .Update(user);

        await context.SaveChangesAsync(cancellationToken);
    }
}