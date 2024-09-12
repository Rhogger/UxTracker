using RestSharp;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;

namespace UxTracker.Core.Contexts.Research.Handlers;

public interface IResearchContextHandler
{
    public Task<RestResponse<Create.Response>?> CreateProject(Create.Request requestModel);
}