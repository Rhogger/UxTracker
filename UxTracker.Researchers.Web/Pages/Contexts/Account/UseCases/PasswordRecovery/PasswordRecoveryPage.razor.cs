using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using PasswordRecoveryVerifyUseCase = UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;
using ResendResetCodeUseCase = UxTracker.Core.Contexts.Account.UseCases.ResendResetCode;

namespace UxTracker.Researchers.Web.Pages.Contexts.Account.UseCases.PasswordRecovery;

public class PasswordRecovery : ComponentBase
{
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    
    protected readonly PasswordRecoveryVerifyUseCase.Request Request = new();

    protected override async Task OnInitializedAsync() =>
        Request.Email = await LocalStorage.GetItemAsync<string>("email") ?? string.Empty;

    protected async Task VerifyAsync()
    {
        try
        {
            var response = await AccountContextHandler.PasswordRecoveryVerifyAsync(Request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Snackbar.Add(response.Data!.Message, Severity.Success);
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
    }
    
    protected async Task ResendResetCodeAsync()
    {
        ResendResetCodeUseCase.Request request = new(Request.Email);
        
        try
        {
            var response = await AccountContextHandler.ResendResetCodeAsync(request);

            if (response is not null)
                if (response.IsSuccessful)
                    Snackbar.Add(response.Data!.Message, Severity.Success);
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