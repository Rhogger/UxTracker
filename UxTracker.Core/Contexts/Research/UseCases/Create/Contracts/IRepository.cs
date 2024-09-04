using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Core.Contexts.Research.UseCases.Create.Contracts;

public interface IRepository
{
    Task SaveAsync(Project project, CancellationToken cancellationToken);
}