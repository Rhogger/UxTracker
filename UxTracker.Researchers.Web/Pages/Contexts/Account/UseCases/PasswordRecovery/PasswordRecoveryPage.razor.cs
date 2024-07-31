using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestSharp;
using UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;

namespace UxTracker.Researchers.Web.Pages.Contexts.Account.UseCases.PasswordRecovery;

public partial class PasswordRecovery : ComponentBase
{
    [Inject] protected NavigationManager Navigation { get; set; } 
    [Inject] protected ISnackbar Snackbar { get; set; }
    [Inject] protected IRestClient RestClient { get; set; }
    
    protected MudForm Form;
    protected string[] Errors = Array.Empty<string>();
    protected bool IsValid;
    
    protected Request Req = new();
    
    protected async Task VerifyAsync()
    {
        var request = new RestRequest("/api/v1/password-recover/verify", Method.Patch)
            .AddJsonBody(Req);
        try
        {
            var response = await RestClient.ExecuteAsync<Response>(request);

            if (response.Data is not null)
            {
                if (response.IsSuccessful)
                {
                    if (response.Data.StatusCode == 200)
                    {
                        Snackbar.Add(response.Data.Message, Severity.Success);
                        Navigation.NavigateTo("/recover/reset-password");
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
    
    protected async Task ResendResetCodeAsync()
    {
        //TODO: Alterar essa passagem de parametros como o email que está nos cookies
        Core.Contexts.Account.UseCases.ResendResetCode.Request req = new(Req.Email);
        
        var request = new RestRequest("/api/v1/password-recover/resend-reset-code", Method.Patch)
            .AddJsonBody(req);
        
        try
        {
            var response = await RestClient.ExecuteAsync<Core.Contexts.Account.UseCases.ResendResetCode.Response>(request);

            if (response.Data is not null)
            {
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                        Snackbar.Add(response.Data.Message, Severity.Success);
                    else
                        Snackbar.Add($"Erro: {response.Data.StatusCode} - {response.Data.Message}", Severity.Error);
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