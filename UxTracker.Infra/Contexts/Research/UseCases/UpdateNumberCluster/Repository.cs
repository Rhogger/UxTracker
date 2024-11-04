using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.UpdateNumberCluster.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.UpdateNumberCluster;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken) => await context
        .Projects
        .AsNoTracking()
        .Include(x => x.Relatories)
        .Include(x => x.Reviews)
        .FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);

    public void AttachProject(Project project) => context.Projects.Attach(project);

    public async Task UpdateProjectAsync(Project project, CancellationToken cancellationToken)
    {
        context
            .Projects
            .Update(project);

        await context.SaveChangesAsync(cancellationToken);
    }
}