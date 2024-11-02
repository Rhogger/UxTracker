namespace UxTracker.Core.Contexts.Research.UseCases.Create.Contracts;

public interface IService
{
    Task CreateBlobAsync(Stream fileStream, string projectId, string fileName);
}