using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestSharp;
using UxTracker.Core;
using UxTracker.Core.Contexts.Account.Validations;

namespace UxTracker.Researchers.Web.Pages.Contexts.Account.UseCases.Verify;

public partial class AccountVerification: ComponentBase
{
    [Inject] protected NavigationManager Navigation { get; set; } 
    [Inject] protected ISnackbar Snackbar { get; set; }
    [Inject] protected IRestClient RestClient { get; set; }
    
    //TODO: Alterar essa passagem de parametros como o email que está nos cookies
    protected Core.Contexts.Account.UseCases.Verify.Request Req = new();
    
    protected MudForm Form;
    protected string[] Errors = Array.Empty<string>();
    protected bool IsValid;

    protected async Task VerifyAsync()
    {
        var request = new RestRequest("/api/v1/verify", Method.Patch)
            .AddJsonBody(Req);
        try
        {
            var response = await RestClient.ExecuteAsync<Core.Contexts.Account.UseCases.Verify.Response>(request);

            if (response.Data is not null)
            {
                if (response.IsSuccessful)
                {
                    if (response.Data.StatusCode == 200)
                    {
                        Snackbar.Add(response.Data.Message, Severity.Success);
                        Navigation.NavigateTo("/");
                    }
                    else
                        Snackbar.Add($"Erro: {response.Data.StatusCode} - {response.Data.Message}", Severity.Error);
                
                }
                else
                {
                    if (response.Data.Notifications is not null)
                        foreach (var notification in response.Data.Notifications)
                            Snackbar.Add(notification.Message, Severity.Error);
                    else
                        Snackbar.Add($"Erro: {response.Data.StatusCode} - {response.Data.Message}", Severity.Error);
                }
            }
            else
                Snackbar.Add($"Erro: {response.StatusCode} - {response.Content}", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Exception: {ex.Message}", Severity.Error);
        }
    }

    protected async Task ResendVerificationCodeAsync()
    {
        //TODO: Alterar essa passagem de parametros como o email que está nos cookies
        //TODO: Corrigir validação, porque essa requisição só pode ser feita se o campo do codigo de verificação for validado
        Core.Contexts.Account.UseCases.ResendVerificationCode.Request req = new(Req.Email);
        
        var request = new RestRequest("/api/v1/resend-verification-code", Method.Patch)
            .AddJsonBody(req);
        
        try
        {
            var response = await RestClient.ExecuteAsync<Core.Contexts.Account.UseCases.ResendVerificationCode.Response>(request);

            if (response is { IsSuccessful: true, Data: not null })
            {
                if (response.Data.StatusCode == 200)
                    Snackbar.Add(response.Data.Message, Severity.Success);
                else
                {
                    if (response.Data.Notifications is not null)
                        foreach (var notification in response.Data.Notifications)
                            Snackbar.Add(notification.Message, Severity.Error);
                    else
                        Snackbar.Add($"Erro: {response.Data.StatusCode} - {response.Data.Message}", Severity.Error);
                }
            }
            else
                Snackbar.Add($"Erro: {response.StatusCode} - {response.Content}", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Exception: {ex.Message}", Severity.Error);
        }
    }
    

}