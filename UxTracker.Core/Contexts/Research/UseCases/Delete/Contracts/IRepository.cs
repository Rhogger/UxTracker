using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Core.Contexts.Research.UseCases.Delete.Contracts;

public interface IRepository
{
    Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken);
    Task DeleteProjectAsync(Project project, CancellationToken cancellationToken);
    Task CreateTransactionAsync(CancellationToken cancellationToken);
    Task RollbackAsync(CancellationToken cancellationToken);
    Task CommitAsync(CancellationToken cancellationToken);
}