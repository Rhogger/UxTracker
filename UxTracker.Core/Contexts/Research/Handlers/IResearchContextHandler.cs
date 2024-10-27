using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using RestSharp;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;
using Update = UxTracker.Core.Contexts.Research.UseCases.Update;
using Delete = UxTracker.Core.Contexts.Research.UseCases.Delete;
using Get = UxTracker.Core.Contexts.Research.UseCases.Get;
using GetAll = UxTracker.Core.Contexts.Research.UseCases.GetAll;
using GetForReview = UxTracker.Core.Contexts.Research.UseCases.GetForReview;
using GetRelatories = UxTracker.Core.Contexts.Research.UseCases.GetRelatories;

namespace UxTracker.Core.Contexts.Research.Handlers;

public interface IResearchContextHandler
{
    public Task<RestResponse<Create.Response>?> CreateProjectAsync(Create.Request requestModel, IBrowserFile file);

    public Task<RestResponse<Update.Response>?>
        UpdateProjectAsync(Update.Request requestModel, IBrowserFile? file);
    public Task<RestResponse<Delete.Response>?> DeleteProjectAsync(string projectId);
    public Task<RestResponse<GetAll.Response>?> GetProjectsAsync();
    public Task<RestResponse<Get.Response>?> GetProjectAsync(string projectId);
    public Task<RestResponse<GetForReview.Response>?> GetProjectForReviewAsync(string projectId);
    public Task<RestResponse<GetRelatories.Response>?> GetRelatoriesAsync();
    public Task GetConsentTermAsync(string projectId, string fileName, IJSRuntime jsRuntime);
}