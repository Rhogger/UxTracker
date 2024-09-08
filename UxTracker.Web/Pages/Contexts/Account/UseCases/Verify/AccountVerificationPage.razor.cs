using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Security;
using VerifyUseCase = UxTracker.Core.Contexts.Account.UseCases.Verify;
using ResendVerificationCodeUseCase = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCode;

namespace UxTracker.Web.Pages.Contexts.Account.UseCases.Verify;

public class AccountVerification: ComponentBase
{
    [Inject] protected IBlazorAuthenticationStateProvider BlazorAuthenticationStateProvider { get; set; } = null!;
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    
    protected readonly VerifyUseCase.Request Request = new();

    protected override async Task OnInitializedAsync() =>
        Request.Email = await LocalStorage.GetItemAsync<string>("email") ?? string.Empty;

    protected async Task VerifyAsync()
    {
        try
        {
            var response = await AccountContextHandler.VerifyAsync(Request);

            if (response is not null)
                if (response.IsSuccessful)
                    if (response.Data!.StatusCode == 200)
                    {
                        BlazorAuthenticationStateProvider.NotifyAuthenticationStateChanged();   

                        Snackbar.Add(response.Data.Message, Severity.Success);
                        Navigation.NavigateTo("/projects");
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
                    Snackbar.Add($"Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"{ex.Message}", Severity.Error);
        }
    }

    protected async Task ResendVerificationCodeAsync()
    {
        ResendVerificationCodeUseCase.Request request = new(Request.Email);
        
        try
        {
            var response = await AccountContextHandler.ResendVerificationCodeAsync(request);

            if (response is not null)
                if (response.IsSuccessful)
                    if (response.Data!.StatusCode == 200)
                        Snackbar.Add(response.Data.Message, Severity.Success);
                    else
                    {
                        if (response.Data.Notifications is not null)
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