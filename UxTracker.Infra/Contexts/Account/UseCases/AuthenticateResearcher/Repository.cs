using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.AuthenticateResearcher.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.AuthenticateResearcher;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Researcher?> GetUserByEmailAsync(string email, CancellationToken cancellationToken) 
        => await context
            .Researchers
            .Include(x => x.Roles)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Email.Address == email && x.IsActive == true, cancellationToken);
}