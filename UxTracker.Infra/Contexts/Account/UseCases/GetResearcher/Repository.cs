using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.GetResearcher.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.GetResearcher;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Researcher?> GetUserByIdAsync(string id, CancellationToken cancellationToken) 
        => await context
            .Researchers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == true, cancellationToken);
}