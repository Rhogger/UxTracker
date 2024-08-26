using Blazored.LocalStorage;
using RestSharp;
using UxTracker.Core.Contexts.Account.Handlers;
using Authenticate = UxTracker.Core.Contexts.Account.UseCases.Authenticate;
using Create = UxTracker.Core.Contexts.Account.UseCases.Create;
using GetUser = UxTracker.Core.Contexts.Account.UseCases.GetUser;
using PasswordRecovery = UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery;
using PasswordRecoveryVerify = UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;
using ResendResetCode = UxTracker.Core.Contexts.Account.UseCases.ResendResetCode;
using ResendVerificationCode = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCode;
using UpdateAccount = UxTracker.Core.Contexts.Account.UseCases.UpdateAccount;
using UpdatePassword = UxTracker.Core.Contexts.Account.UseCases.UpdatePassword;
using Verify = UxTracker.Core.Contexts.Account.UseCases.Verify;

namespace UxTracker.Researchers.Web.Handlers;

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
    
    public async Task<RestResponse<Authenticate.Response>?> SignInAsync(Authenticate.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/authenticate", Method.Post)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<Authenticate.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        if (await LocalStorage.ContainKeyAsync("email") == false)
                            await LocalStorage.SetItemAsync("email", requestModel.Email);

                        await CookieHandler.SaveAuthToken(response.Data.Data!.Token);
                        return response;
                    }
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
    
    public async Task<RestResponse<Create.Response>?> SignUpAsync(Create.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/create", Method.Post)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<Create.Response>(request);

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
            throw new Exception($"Status Code {response.StatusCode} - Conteúdo: {response.Content}");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }

    public async Task SignOutAsync() => await CookieHandler.RemoveAuthTokenAsync();

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
                        if (await LocalStorage.ContainKeyAsync("email") == false)
                            await LocalStorage.SetItemAsync("email", requestModel.Email);

                        return response;
                    }
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

    public async Task<RestResponse<Verify.Response>?> VerifyAsync(Verify.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/verify", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<Verify.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                    {
                        await CookieHandler.SaveAuthToken(response.Data.Data!.Token);
   
                        return response;
                    }
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

    public async Task<RestResponse<ResendVerificationCode.Response>?> ResendVerificationCodeAsync(ResendVerificationCode.Request requestModel)
    {
        var request = new RestRequest("/api/v1/users/researchers/verify/resend", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<ResendVerificationCode.Response>(request);

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
            throw new Exception($"Status Code {response.StatusCode} - Conteúdo: {response.Content}");
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
            throw new Exception($"Status Code {response.StatusCode} - Conteúdo: {response.Content}");
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
                        if (await LocalStorage.ContainKeyAsync("email") == false)
                            await LocalStorage.SetItemAsync("email", requestModel.Email);

                        return response;
                    }
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
                        if (await LocalStorage.ContainKeyAsync("email") == false)
                            await LocalStorage.SetItemAsync("email", requestModel.Email);
   
                        return response;
                    }
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
    
    public async Task<RestResponse<UpdatePassword.Response>?> UpdateAccountPasswordAsync(UpdatePassword.Request requestModel)
    {
        var request = new RestRequest("/api/v1/account/update-password", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<UpdatePassword.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                        return response;
                    else
                        throw new Exception(
                            $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");
                else
                    throw new Exception(
                        $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");

            throw new Exception($"Status Code {response.StatusCode} - Conteúdo: {response.Content}");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    
    public async Task<RestResponse<GetUser.Response>?> GetUserAsync()
    {
        try
        {
            var request = new RestRequest("/api/v1/account/");

            var token = await CookieHandler.GetAuthToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                throw new Exception("Token JWT não encontrado.");
            }
        
            var response = await RestClient.ExecuteAsync<GetUser.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                        return response;
                    else
                        throw new Exception(
                            $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");
                else
                    throw new Exception(
                        $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");

            throw new Exception($"Status Code {response.StatusCode} - Conteúdo: {response.Content}");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    
    public async Task<RestResponse<UpdateAccount.Response>?> UpdateAccountAsync(UpdateAccount.Request requestModel)
    {
        var request = new RestRequest("/api/v1/account/", Method.Patch)
            .AddJsonBody(requestModel);

        try
        {
            var response = await RestClient.ExecuteAsync<UpdateAccount.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                        return response;
                    else
                        throw new Exception(
                            $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");
                else
                    throw new Exception(
                        $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");

            throw new Exception($"Status Code {response.StatusCode} - Conteúdo: {response.Content}");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
}