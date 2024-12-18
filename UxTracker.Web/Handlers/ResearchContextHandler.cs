using System.Net.Http.Headers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using RestSharp;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Research.Handlers;
using Create = UxTracker.Core.Contexts.Research.UseCases.Create;
using Update = UxTracker.Core.Contexts.Research.UseCases.Update;
using UpdateStatus = UxTracker.Core.Contexts.Research.UseCases.UpdateStatus;
using UpdateNumberCluster = UxTracker.Core.Contexts.Research.UseCases.UpdateNumberCluster;
using Delete = UxTracker.Core.Contexts.Research.UseCases.Delete;
using Get = UxTracker.Core.Contexts.Research.UseCases.Get;
using GetAll = UxTracker.Core.Contexts.Research.UseCases.GetAll;
using GetForReview = UxTracker.Core.Contexts.Research.UseCases.GetForReview;
using GetRelatories = UxTracker.Core.Contexts.Research.UseCases.GetRelatories;

namespace UxTracker.Web.Handlers;

public class ResearchContextHandler(
    IAccountContextHandler accountContextHandler,
    IRestClient restClient,
    ICookieHandler cookieHandler,
    NavigationManager navigation)
    : IResearchContextHandler
{
    public async Task<RestResponse<Create.Response>?> CreateProjectAsync(Create.Request requestModel, IBrowserFile file)
    {
        var request = new RestRequest("/api/v1/projects/", Method.Post);

        var token = await cookieHandler.GetAccessToken();
        if (!string.IsNullOrEmpty(token?.Value))
        {
            request.AddHeader("Authorization", $"Bearer {token.Value}");
        }
        else
        {
            var refreshToken = await cookieHandler.GetRefreshToken();
            if (!string.IsNullOrEmpty(refreshToken?.Value))
            {
                await accountContextHandler.RefreshTokenAsync();
                    
                var newToken = await cookieHandler.GetAccessToken();
                    
                request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
            }
            else
            {
                navigation.NavigateTo("/login");
                throw new Exception("Necessário logar novamente.");
            }
        }
        
        request.AddParameter("title", requestModel.Title);
        request.AddParameter("description", requestModel.Description);
        request.AddParameter("periodType", requestModel.PeriodType);
        request.AddParameter("surveyCollections", requestModel.SurveyCollections);
        if (requestModel.StartDate.HasValue)
        {
            request.AddParameter("startDate", requestModel.StartDate.Value.ToString("o"));
        }

        if (requestModel.EndDate.HasValue)
        {
            request.AddParameter("endDate", requestModel.EndDate.Value.ToString("o"));
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

        try
        {
            var response = await restClient.ExecuteAsync<Create.Response>(request);

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
    
    public async Task<RestResponse<Update.Response>?> UpdateProjectAsync(Update.Request requestModel, IBrowserFile? file)
    {
        var request = new RestRequest($"/api/v1/projects/{requestModel.ProjectId}", Method.Patch);

        var token = await cookieHandler.GetAccessToken();
        if (!string.IsNullOrEmpty(token?.Value))
        {
            request.AddHeader("Authorization", $"Bearer {token.Value}");
        }
        else
        {
            var refreshToken = await cookieHandler.GetRefreshToken();
            if (!string.IsNullOrEmpty(refreshToken?.Value))
            {
                await accountContextHandler.RefreshTokenAsync();
                    
                var newToken = await cookieHandler.GetAccessToken();
                    
                request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
            }
            else
            {
                navigation.NavigateTo("/login");
                throw new Exception("Necessário logar novamente.");
            }
        }
        
        request.AddParameter("title", requestModel.Title);
        request.AddParameter("description", requestModel.Description);
        if (requestModel.StartDate.HasValue)
        {
            request.AddParameter("startDate", requestModel.StartDate.Value.ToString("o"));
        }
        if (requestModel.EndDate.HasValue)
        {
            request.AddParameter("endDate", requestModel.EndDate.Value.ToString("o"));
        }
        request.AddParameter("periodType", requestModel.PeriodType);
        request.AddParameter("surveyCollections", requestModel.SurveyCollections);
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
        
        if (file is not null)
        {
            request.AddFile("consentTerm", () => file.OpenReadStream(2 * 1024 * 1024), file.Name, file.ContentType);
        }
        else
        {
            var emptyStream = new MemoryStream(); 
            var emptyContent = new StreamContent(emptyStream);
            emptyContent.Headers.ContentType = new MediaTypeHeaderValue("application/pdf"); 
            emptyContent.Headers.ContentLength = 0;

            request.AddFile("consentTerm", () => emptyContent.ReadAsStream(), "empty.pdf", "application/pdf");
        }

        try
        {
            var response = await restClient.ExecuteAsync<Update.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
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

            var token = await cookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await cookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await accountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await cookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    navigation.NavigateTo("/login");
                    throw new Exception("Necessário logar novamente.");
                }
            }

            var response = await restClient.ExecuteAsync<GetAll.Response>(request);

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

    public async Task<RestResponse<GetForReview.Response>?> GetProjectForReviewAsync(string projectId)
    {
        try
        {
            var request = new RestRequest($"/api/v1/review/{projectId}");

            var token = await cookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await cookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await accountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await cookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    navigation.NavigateTo("/login");
                    throw new Exception("Necessário logar novamente.");
                }
            }

            var response = await restClient.ExecuteAsync<GetForReview.Response>(request);

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

    public async Task<RestResponse<Get.Response>?> GetProjectAsync(string projectId)
    {
        try
        {
            var request = new RestRequest($"/api/v1/projects/{projectId}");

            var token = await cookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await cookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await accountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await cookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    navigation.NavigateTo("/login");
                    throw new Exception("Necessário logar novamente.");
                }
            }

            var response = await restClient.ExecuteAsync<Get.Response>(request);

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

            var token = await cookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await cookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await accountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await cookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    navigation.NavigateTo("/login");
                    throw new Exception("Necessário logar novamente.");
                }
            }

            var response = await restClient.ExecuteAsync<GetRelatories.Response>(request);

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
    
    public async Task GetConsentTermAsync(string projectId, string? fileName, IJSRuntime jsRuntime)
    {
        var request = new RestRequest($"/api/v1/terms/{projectId}");

        try
        {
            var response = await restClient.ExecuteAsync(request);

            if (response is { IsSuccessful: true, RawBytes: not null })
            {
                var byteArray = response.RawBytes;
                var base64String = Convert.ToBase64String(byteArray);
                var dataUrl = $"data:application/pdf;base64,{base64String}";

                if (!string.IsNullOrEmpty(dataUrl))
                {
                    await jsRuntime.InvokeVoidAsync("setIframeSrc", "consent-term", dataUrl);
                }
                else
                {
                    throw new Exception("Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    public async Task DownloadConsentTermAsync(string projectId, string? fileName, IJSRuntime jsRuntime)
    {
        var request = new RestRequest($"/api/v1/terms/{projectId}");

        try
        {
            var response = await restClient.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                var byteArray = response.RawBytes;

                var dataUrl = string.Empty;

                if (byteArray is not null)
                {
                    var base64 = Convert.ToBase64String(byteArray);
                    dataUrl = $"data:application/pdf;base64,{base64}";
                }

                if (!string.IsNullOrEmpty(dataUrl))
                {
                    await jsRuntime.InvokeVoidAsync("downloadFile", [dataUrl, fileName]);
                }
                else
                {
                    throw new Exception("Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.");
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    
    public async Task<RestResponse<Delete.Response>?> DeleteProjectAsync(string projectId)
    {
        try
        {
            var request = new RestRequest($"/api/v1/projects/{projectId}", Method.Delete);

            var token = await cookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await cookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await accountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await cookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    navigation.NavigateTo("/login");
                    throw new Exception("Necessário logar novamente.");
                }
            }

            var response = await restClient.ExecuteAsync<Delete.Response>(request);

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
    
    public async Task<RestResponse<UpdateStatus.Response>?> UpdateStatusAsync(UpdateStatus.Request requestModel)
    {
        try
        {
            var request = new RestRequest($"/api/v1/projects/{requestModel.ProjectId}/status", Method.Patch)
                .AddJsonBody(requestModel);

            var token = await cookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await cookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await accountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await cookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    navigation.NavigateTo("/login");
                    throw new Exception("Necessário logar novamente.");
                }
            }

            var response = await restClient.ExecuteAsync<UpdateStatus.Response>(request);

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
    
    public async Task<RestResponse<UpdateNumberCluster.Response>?> UpdateNumberClusterAsync(UpdateNumberCluster.Request requestModel)
    {
        try
        {
            var request = new RestRequest($"/api/v1/projects/{requestModel.ProjectId}/cluster", Method.Patch)
                .AddJsonBody(requestModel);

            var token = await cookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await cookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await accountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await cookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    navigation.NavigateTo("/login");
                    throw new Exception("Necessário logar novamente.");
                }
            }

            var response = await restClient.ExecuteAsync<UpdateNumberCluster.Response>(request);

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