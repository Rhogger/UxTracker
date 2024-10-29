using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.UpdateStatus.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.UpdateStatus;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<List<Relatory>?> GetRelatoriesByIdAsync(List<string> relatories, CancellationToken cancellationToken) => 
        await context
            .Relatories
            .Where(x => relatories
                .Contains(x.Id.ToString()))
            .ToListAsync(cancellationToken);

    public async Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken) => await context
        .Projects
        .AsNoTracking()
        .Include(x => x.Relatories)
        .Include(x => x.Reviews)
        .FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);

    public void AttachProject(Project project) => context.Projects.Attach(project);
    public void AttachRelatories(List<Relatory> relatories) => context.Relatories.AttachRange(relatories);

    public async Task UpdateProjectAsync(Project project, CancellationToken cancellationToken)
    {
        context
            .Projects
            .Update(project);

        await context.SaveChangesAsync(cancellationToken);
    }
}