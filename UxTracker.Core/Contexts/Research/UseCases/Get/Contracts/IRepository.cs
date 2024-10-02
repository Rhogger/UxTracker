using UxTracker.Core.Contexts.Research.Entities;

namespace UxTracker.Core.Contexts.Research.UseCases.Get.Contracts;

public interface IRepository
{
    Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken);
}