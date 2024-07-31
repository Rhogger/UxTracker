using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestSharp;
using UxTracker.Core.Contexts.Account.UseCases.Authenticate;

namespace UxTracker.Researchers.Web.Pages.Contexts.Account.UseCases.Authenticate;

public partial class Login : ComponentBase
{
    [Inject] protected NavigationManager Navigation { get; set; } 
    [Inject] protected ISnackbar Snackbar { get; set; }
    [Inject] protected IRestClient RestClient { get; set; }
    
    protected Request Req = new();
    
    protected MudForm Form;
    protected string[] Errors = Array.Empty<string>();
    protected bool IsValid;
    
    protected InputType PasswordInput = InputType.Password;
    protected string PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
    protected bool IsPasswordShow;

    //TODO: Utilizar cookies para armazenar o token jwt
    protected async Task AuthenticateAsync()
    {
        var request = new RestRequest("/api/v1/authenticate", Method.Post)
            .AddJsonBody(Req);
        try
        {
            var response = await RestClient.ExecuteAsync<Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                        Navigation.NavigateTo("/");
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
            else
                Snackbar.Add($"Erro: {response.StatusCode} - {response.Content}", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Exception: {ex.Message}", Severity.Error);
        }
    }

    //TODO: Ajustar aqui, ele deve atualizar a data de expiração quando gera um novo codigo
    protected async Task SendResetCodeAsync()
    {
        var request = new RestRequest("/api/v1/recover-password", Method.Patch)
            .AddJsonBody(Req);
        
        try
        {
            var response = await RestClient.ExecuteAsync<Core.Contexts.Account.UseCases.PasswordRecovery.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                {
                    if (response.Data.StatusCode == 200)
                    {
                        Snackbar.Add(response.Data.Message, Severity.Success);
                        Navigation.NavigateTo("/recover");
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
            else
                Snackbar.Add($"Erro: {response.StatusCode} - {response.Content}", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"Exception: {ex.Message}", Severity.Error);
        }
    }
    
    public void ShowPassword()
    {
        if (IsPasswordShow)
        {
            IsPasswordShow = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInput = InputType.Password;
        }
        else
        {
            IsPasswordShow = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInput = InputType.Text;
        }
    }
}