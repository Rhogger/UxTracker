using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using PasswordRecoveryVerifyUseCase = UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;
using ResendResetCodeUseCase = UxTracker.Core.Contexts.Account.UseCases.ResendResetCode;

namespace UxTracker.Web.Pages.Contexts.Account.UseCases.PasswordRecovery;

public class PasswordRecovery : ComponentBase
{
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    
    protected readonly PasswordRecoveryVerifyUseCase.Request Request = new();
    protected bool IsBusy { get; set; }
    protected bool IsBusyResend { get; private set; }

    protected override async Task OnInitializedAsync() =>
        Request.Email = await LocalStorage.GetItemAsync<string>("email") ?? string.Empty;

    protected async Task VerifyAsync()
    {
        try
        {
            IsBusy = true;
            var response = await AccountContextHandler.PasswordRecoveryVerifyAsync(Request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    var message = response.Data!.Message;
                    if (message != null) Snackbar.Add(message, Severity.Success);
                    Navigation.NavigateTo("/recover/reset-password");
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
        finally
        {
            IsBusy = false;
        }
    }
    
    protected async Task ResendResetCodeAsync()
    {
        ResendResetCodeUseCase.Request request = new(Request.Email);

        try
        {
            IsBusyResend = true;

            var response = await AccountContextHandler.ResendResetCodeAsync(request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    var message = response.Data!.Message;
                    if (message != null) Snackbar.Add(message, Severity.Success);
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
        finally
        {
            IsBusyResend = false;
        }
    }
    
    protected void NavigateToLogin() => Navigation.NavigateTo("/login");
}