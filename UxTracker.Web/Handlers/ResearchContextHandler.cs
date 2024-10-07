using RestSharp;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Research.Handlers;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;
using GetAll = UxTracker.Core.Contexts.Research.UseCases.GetAll;

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
    
    public async Task<RestResponse<Create.Response>?> CreateProject(Create.Request requestModel)
    {
        var request = new RestRequest("/api/v1/projects/create/", Method.Post)
            .AddJsonBody(requestModel);

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
}