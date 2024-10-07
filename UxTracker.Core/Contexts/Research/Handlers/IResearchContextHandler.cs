using RestSharp;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;
using GetAll = UxTracker.Core.Contexts.Research.UseCases.GetAll;

namespace UxTracker.Core.Contexts.Research.Handlers;

public interface IResearchContextHandler
{
    public Task<RestResponse<Create.Response>?> CreateProject(Create.Request requestModel);
    public Task<RestResponse<GetAll.Response>?> GetProjectsAsync();
}