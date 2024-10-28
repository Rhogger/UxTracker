using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.Create.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.Create;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<List<Relatory>?> GetRelatoriesByIdAsync(List<string> relatories, CancellationToken cancellationToken) => 
        await context
            .Relatories
            .AsNoTracking()
            .Where(x => relatories
                .Contains(x.Id.ToString()))
            .ToListAsync(cancellationToken);
    
    public void AttachRelatories(List<Relatory> relatories) => context.Relatories.AttachRange(relatories);

    public async Task SaveAsync(Project project, CancellationToken cancellationToken)
    {
        await context.Projects.AddAsync(project, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}