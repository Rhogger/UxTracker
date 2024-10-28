using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.GetReviewer.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Account.UseCases.GetReviewer;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Reviewer?> GetUserByIdAsync(string id, CancellationToken cancellationToken) 
        => await context
            .Reviewers
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id.ToString() == id && x.IsActive == true, cancellationToken);
}