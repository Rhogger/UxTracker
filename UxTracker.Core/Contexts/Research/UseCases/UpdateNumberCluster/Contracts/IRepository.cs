using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Core.Contexts.Research.UseCases.UpdateNumberCluster.Contracts;

public interface IRepository
{
    Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken);
    void AttachProject(Project project);
    Task UpdateProjectAsync(Project project, CancellationToken cancellationToken);
}