using Blazored.LocalStorage;
using RestSharp;
using UxTracker.Core.Contexts.Account.Handlers;
using AuthenticateResearcher = UxTracker.Core.Contexts.Account.UseCases.AuthenticateResearcher;
using AuthenticateReviewer = UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer;
using CreateResearcher = UxTracker.Core.Contexts.Account.UseCases.CreateResearcher;
using CreateReviewer = UxTracker.Core.Contexts.Account.UseCases.CreateReviewer;
using DeleteResearcher = UxTracker.Core.Contexts.Account.UseCases.DeleteResearcher;
using DeleteReviewer = UxTracker.Core.Contexts.Account.UseCases.DeleteReviewer;
using GetResearcher = UxTracker.Core.Contexts.Account.UseCases.GetResearcher;
using GetReviewer = UxTracker.Core.Contexts.Account.UseCases.GetReviewer;
using PasswordRecovery = UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery;
using PasswordRecoveryVerify = UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;
using RefreshToken = UxTracker.Core.Contexts.Account.UseCases.RefreshToken;
using ResendResetCode = UxTracker.Core.Contexts.Account.UseCases.ResendResetCode;
using ResendVerificationCodeResearcher = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeResearcher;
using ResendVerificationCodeReviewer = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeReviewer;
using UpdateResearcher = UxTracker.Core.Contexts.Account.UseCases.UpdateResearcher;
using UpdateReviewer = UxTracker.Core.Contexts.Account.UseCases.UpdateReviewer;
using UpdatePassword = UxTracker.Core.Contexts.Account.UseCases.UpdatePassword;
using VerifyResearcher = UxTracker.Core.Contexts.Account.UseCases.VerifyResearcher;
using VerifyReviewer = UxTracker.Core.Contexts.Account.UseCases.VerifyReviewer;

namespace UxTracker.Web.Handlers;

public class AccountContextHandler: IAccountContextHandler
{
    private readonly IRestClient RestClient;
    private readonly ICookieHandler CookieHandler;
    private readonly ILocalStorageService LocalStorage;


    public AccountContextHandler(IRestClient restClient,
        ICookieHandler cookieHandler, ILocalStorageService localStorage)
    {
        RestClient = restClient;
        CookieHandler = cookieHandler;
        LocalStorage = localStorage;
    }
    
    public async Task<RestResponse<AuthenticateResearcher.Response>?> SignInResearcherAsync(AuthenticateResearcher.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/authenticate", Method.Post)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<AuthenticateResearcher.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        await LocalStorage.SetItemAsync("email", requestModel.Email);

                        await CookieHandler.SaveAccessToken(response.Data.Data!.AccessToken);
                        await CookieHandler.SaveRefreshToken(response.Data.Data!.RefreshToken);

                        return response;
                    }
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

    public async Task<RestResponse<AuthenticateReviewer.Response>?> SignInReviewerAsync(AuthenticateReviewer.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/reviewers/authenticate", Method.Post)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<AuthenticateReviewer.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        await LocalStorage.SetItemAsync("email", requestModel.Email);
                        
                        await LocalStorage.SetItemAsync("researchCode", requestModel.ResearchCode);

                        await CookieHandler.SaveAccessToken(response.Data.Data!.Payload.AccessToken);
                        await CookieHandler.SaveRefreshToken(response.Data.Data!.Payload.RefreshToken);

                        return response;
                    }
                    else
                        throw new Exception(
                            $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");
                else
                {
                    await LocalStorage.SetItemAsync("email", requestModel.Email);
                        
                    await LocalStorage.SetItemAsync("researchCode", requestModel.ResearchCode);
                    
                    return response;
                }
            
            throw new Exception($"{response.StatusCode} - {response.Content}");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    
    public async Task<RestResponse<GetResearcher.Response>?> GetResearcherAsync()
    {
        try
        {
            var request = new RestRequest("/api/v1/users/researchers/account/");

            var token = await CookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                throw new Exception("Token JWT não encontrado.");
            }
        
            var response = await RestClient.ExecuteAsync<GetResearcher.Response>(request);

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
    
    public async Task<RestResponse<GetReviewer.Response>?> GetReviewerAsync()
    {
        try
        {
            var request = new RestRequest("/api/v1/users/reviewers/account/");

            var token = await CookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                throw new Exception("Token JWT não encontrado.");
            }
        
            var response = await RestClient.ExecuteAsync<GetReviewer.Response>(request);

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
    
    public async Task<RestResponse<CreateResearcher.Response>?> SignUpResearchAsync(CreateResearcher.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/create", Method.Post)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<CreateResearcher.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 201)
                    {
                        await LocalStorage.SetItemAsync("email", requestModel.Email);

                        return response;
                    }
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
    
    public async Task<RestResponse<CreateReviewer.Response>?> SignUpReviewerAsync(CreateReviewer.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/reviewers/create", Method.Post)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<CreateReviewer.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 201)
                    {
                        await LocalStorage.SetItemAsync("email", requestModel.Email);

                        return response;
                    }
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

    public async Task SignOutAsync()
    {
        await CookieHandler.RemoveAccessTokenAsync();
        await CookieHandler.RemoveRefreshTokenAsync();
    } 

    public async Task<RestResponse<VerifyResearcher.Response>?> VerifyResearcherAsync(VerifyResearcher.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/verify", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<VerifyResearcher.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        await CookieHandler.SaveAccessToken(response.Data.Data!.AccessToken);
                        await CookieHandler.SaveRefreshToken(response.Data.Data!.RefreshToken);
   
                        return response;
                    }
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
    
    public async Task<RestResponse<VerifyReviewer.Response>?> VerifyReviewerAsync(VerifyReviewer.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/reviewers/verify", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<VerifyReviewer.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        await CookieHandler.SaveAccessToken(response.Data.Data!.Payload.AccessToken);
                        await CookieHandler.SaveRefreshToken(response.Data.Data!.Payload.RefreshToken);
                        
                        return response;
                    }
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

    public async Task<RestResponse<ResendVerificationCodeResearcher.Response>?> ResendVerificationCodeResearcherAsync(ResendVerificationCodeResearcher.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/verify/resend", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<ResendVerificationCodeResearcher.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        await LocalStorage.SetItemAsync("email", requestModel.Email);

                        return response;
                    }
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
    
    public async Task<RestResponse<ResendVerificationCodeReviewer.Response>?> ResendVerificationCodeReviewerAsync(ResendVerificationCodeReviewer.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/reviewers/verify/resend", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<ResendVerificationCodeReviewer.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        await LocalStorage.SetItemAsync("email", requestModel.Email);

                        return response;
                    }
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

    public async Task<RestResponse<PasswordRecovery.Response>?> SendResetCodeAsync(PasswordRecovery.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/recover", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<PasswordRecovery.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        await LocalStorage.SetItemAsync("email", requestModel.Email);

                        return response;
                    }
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
    
    public async Task<RestResponse<ResendResetCode.Response>?> ResendResetCodeAsync(ResendResetCode.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/recover/resend", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<ResendResetCode.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        return response;
                    }
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

    public async Task<RestResponse<PasswordRecoveryVerify.Response>?> PasswordRecoveryVerifyAsync(PasswordRecoveryVerify.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/recover/verify", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<PasswordRecoveryVerify.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        await LocalStorage.SetItemAsync("email", requestModel.Email);

                        return response;
                    }
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
    
    public async Task<RestResponse<UpdatePassword.Response>?> UpdatePasswordAsync(UpdatePassword.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/recover/update", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<UpdatePassword.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        await LocalStorage.SetItemAsync("email", requestModel.Email);
   
                        return response;
                    }
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
    
    public async Task<RestResponse<UpdateResearcher.Response>?> UpdateResearcherAsync(UpdateResearcher.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/account", Method.Patch)
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
            var response = await RestClient.ExecuteAsync<UpdateResearcher.Response>(request);

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
    
    public async Task<RestResponse<UpdateReviewer.Response>?> UpdateReviewerAsync(UpdateReviewer.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/reviewers/account", Method.Patch)
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
            var response = await RestClient.ExecuteAsync<UpdateReviewer.Response>(request);

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
    
    public async Task<RestResponse<DeleteResearcher.Response>?> DeleteResearcherAsync(DeleteResearcher.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/account/inactivate", Method.Patch)
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
            var response = await RestClient.ExecuteAsync<DeleteResearcher.Response>(request);

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
    
    public async Task<RestResponse<DeleteReviewer.Response>?> DeleteReviewerAsync(DeleteReviewer.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/reviewers/account/inactivate", Method.Patch)
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
            var response = await RestClient.ExecuteAsync<DeleteReviewer.Response>(request);

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
    
    public async Task<RestResponse<RefreshToken.Response>?> RefreshTokenAsync()
    {
        var request = new RestRequest("/api/v1/refresh", Method.Post);
        
        var token = await CookieHandler.GetRefreshToken();
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
            var response = await RestClient.ExecuteAsync<RefreshToken.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        CookieHandler.SaveAccessToken(response.Data.Data.AccessToken);
                        CookieHandler.SaveRefreshToken(response.Data.Data.RefreshToken);
                        return response;
                    }
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