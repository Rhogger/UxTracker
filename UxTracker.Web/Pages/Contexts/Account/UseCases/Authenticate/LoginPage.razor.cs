using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Security;
using AuthenticateUseCase = UxTracker.Core.Contexts.Account.UseCases.Authenticate;
using PasswordRecoveryUseCase = UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery;

namespace UxTracker.Web.Pages.Contexts.Account.UseCases.Authenticate;

public class Login : ComponentBase
{
    [Inject] protected IBlazorAuthenticationStateProvider BlazorAuthenticationStateProvider { get; set; } = null!;
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    
    protected readonly AuthenticateUseCase.Request Request = new();

    protected override async Task OnInitializedAsync() => Request.Email = await LocalStorage.GetItemAsync<string>("email") ?? string.Empty;
    
    protected async Task SignInAsync()
    {
        try
        {
            var response = await AccountContextHandler.SignInAsync(Request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    BlazorAuthenticationStateProvider.NotifyAuthenticationStateChanged();   
                    Navigation.NavigateTo("/account");
                }
                else
                {
                    if (response.Data!.Notifications is not null)
                        foreach (var notification in response.Data.Notifications)
                            Snackbar.Add(notification.Message, Severity.Error);
                    else
                    {
                        if(string.Equals(response.Data.Message, "Conta inativa"))
                            Snackbar.Add($"Erro: {response.Data.StatusCode} - {response.Data.Message}. Por favor, verifique primeiro.", Severity.Error,
                                config =>
                                {
                                    config.Action = "Verificar";
                                    config.ActionColor = Color.Warning;
                                    config.Onclick = snackbar =>
                                    {
                                        Navigation.NavigateTo("/verify");
                                        return Task.CompletedTask;
                                    };
                                });
                        else
                            Snackbar.Add($"Erro: {response.Data.StatusCode} - {response.Data.Message}", Severity.Error);
                    }
                }
            else
                Snackbar.Add($"Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add(ex.Message, Severity.Error);
        }
    }

    protected async Task SendResetCodeAsync()
    {
        PasswordRecoveryUseCase.Request request = new()
        {
            Email = Request.Email
        };

        try
        {
            var response = await AccountContextHandler.SendResetCodeAsync(request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Snackbar.Add(response.Data!.Message, Severity.Success);
                    Navigation.NavigateTo("/recover");
                }
                else
                {
                    if (response.Data!.Notifications is not null)
                        foreach (var notification in response.Data.Notifications)
                            Snackbar.Add(notification.Message, Severity.Error);
                    else
                        Snackbar.Add($"Erro: {response.Data.StatusCode} - {response.Data.Message}", Severity.Error);

                }
            else
                Snackbar.Add($"Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"{ex.Message}", Severity.Error);
        }
    }
}