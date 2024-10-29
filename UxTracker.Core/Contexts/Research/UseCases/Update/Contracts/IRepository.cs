using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Core.Contexts.Research.UseCases.Update.Contracts;

public interface IRepository
{
    Task<List<Relatory>?> GetRelatoriesByIdAsync(List<string> relatories, CancellationToken cancellationToken);
    Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken);
    void AttachProject(Project project);
    void AttachRelatories(List<Relatory> relatories);
    Task UpdateProjectAsync(Project project, CancellationToken cancellationToken);
    Task CreateTransactionAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);

}