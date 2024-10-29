using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Core.Contexts.Research.UseCases.Create.Contracts;

public interface IRepository
{
    Task<List<Relatory>?> GetRelatoriesByIdAsync(List<string> relatories, CancellationToken cancellationToken);
    void AttachRelatories(List<Relatory> relatories);
    Task SaveAsync(Project project, CancellationToken cancellationToken);
}