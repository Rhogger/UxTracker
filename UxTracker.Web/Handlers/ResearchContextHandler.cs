using Microsoft.AspNetCore.Components.Forms;
using RestSharp;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Research.Handlers;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;
using GetAll = UxTracker.Core.Contexts.Research.UseCases.GetAll;
using GetRelatories = UxTracker.Core.Contexts.Research.UseCases.GetRelatories;

namespace UxTracker.Web.Handlers;

public class ResearchContextHandler: IResearchContextHandler
{
    private readonly IRestClient RestClient;
    private readonly ICookieHandler CookieHandler;

    public ResearchContextHandler(IRestClient restClient, ICookieHandler cookieHandler)
    {
        RestClient = restClient;
        CookieHandler = cookieHandler;
    }
    
    public async Task<RestResponse<Create.Response>?> CreateProjectAsync(Create.Request requestModel, IBrowserFile file)
    {
        var request = new RestRequest("/api/v1/projects/create/", Method.Post);

        request.AddParameter("title", requestModel.Title);
        request.AddParameter("description", requestModel.Description);
        request.AddParameter("periodType", requestModel.PeriodType);
        request.AddParameter("surveyCollections", requestModel.SurveyCollections);
        if (requestModel.StartDate.HasValue){
            request.AddParameter("startDate", requestModel.StartDate.Value);
        }
        if (requestModel.EndDate.HasValue)
        {
            request.AddParameter("endDate", requestModel.EndDate.Value);
        }

        if (requestModel.Relatories.Count > 0)
        {
            foreach (var relatory in requestModel.Relatories)
            {
                request.AddParameter("relatories", relatory);
            }
        }
        else
        {
            request.AddParameter("relatories", "");
        }

        request.AddFile("consentTerm", () => file.OpenReadStream(2 * 1024 * 1024), file.Name, file.ContentType);
        
        var token = await CookieHandler.GetAccessToken();
        
        if (!string.IsNullOrEmpty(token?.Value))
        {
            request.AddHeader("Authorization", $"Bearer {token.Value}");
        }
        else
        {
            throw new Exception("Token JWT não encontrado.");
        }
        
        try
        {
            var response = await RestClient.ExecuteAsync<Create.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 201)
                        return response;
                    else
                        throw new Exception(
                            $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");
                else
                    return response;
            throw new Exception($"Status Code {response.StatusCode} - Conteúdo: {response.Content}");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    
    public async Task<RestResponse<GetAll.Response>?> GetProjectsAsync()
    {
        try
        {
            var request = new RestRequest("/api/v1/projects/");

            var token = await CookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                throw new Exception("Token JWT não encontrado.");
            }
        
            var response = await RestClient.ExecuteAsync<GetAll.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                        return response;
                    else
                        throw new Exception(
                            $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");
                else
                    return response;
            throw new Exception($"{response.StatusCode} - {response.Content}");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    
    public async Task<RestResponse<GetRelatories.Response>?> GetRelatoriesAsync()
    {
        try
        {
            var request = new RestRequest("/api/v1/relatories/");

            var token = await CookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                throw new Exception("Token JWT não encontrado.");
            }
        
            var response = await RestClient.ExecuteAsync<GetRelatories.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                        return response;
                    else
                        throw new Exception(
                            $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");
                else
                    return response;
            throw new Exception($"{response.StatusCode} - {response.Content}");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
}