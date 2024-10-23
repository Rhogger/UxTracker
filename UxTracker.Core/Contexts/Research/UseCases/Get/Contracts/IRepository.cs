using UxTracker.Core.Contexts.Research.DTOs;

namespace UxTracker.Core.Contexts.Research.UseCases.Get.Contracts;

public interface IRepository
{
    Task<GetDTO?> GetProjectByIdAsync(string id, CancellationToken cancellationToken);
}