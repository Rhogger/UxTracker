using UxTracker.Core.Contexts.Research.DTOs;

namespace UxTracker.Core.Contexts.Research.UseCases.GetRelatories.Contracts;

public interface IRepository
{
    Task<List<GetRelatoriesDTO>?> GetRelatoriesAsync(CancellationToken cancellationToken);
}