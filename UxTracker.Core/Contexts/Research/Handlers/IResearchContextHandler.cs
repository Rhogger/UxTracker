using Microsoft.AspNetCore.Components.Forms;
using RestSharp;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;
using GetAll = UxTracker.Core.Contexts.Research.UseCases.GetAll;
using GetRelatories = UxTracker.Core.Contexts.Research.UseCases.GetRelatories;

namespace UxTracker.Core.Contexts.Research.Handlers;

public interface IResearchContextHandler
{
    public Task<RestResponse<Create.Response>?> CreateProjectAsync(Create.Request requestModel, IBrowserFile file);
    public Task<RestResponse<GetAll.Response>?> GetProjectsAsync();
    public Task<RestResponse<GetRelatories.Response>?> GetRelatoriesAsync();
}