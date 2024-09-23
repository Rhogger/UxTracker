using UxTracker.Core.Contexts.Research.DTOs;

namespace UxTracker.Core.Contexts.Research.UseCases.GetAll.Contracts;

public interface IRepository
{
    Task<List<GetAllProjectDTO>?> GetProjectsByUser(string userId, CancellationToken cancellationToken);
}