using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Security;
using VerifyResearcherUseCase = UxTracker.Core.Contexts.Account.UseCases.VerifyResearcher;
using ResendVerificationCodeResearcherUseCase = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeResearcher;

namespace UxTracker.Web.Pages.Contexts.Account.UseCases.VerifyResearcher;

public class AccountVerification: ComponentBase
{
    [Inject] protected IBlazorAuthenticationStateProvider BlazorAuthenticationStateProvider { get; set; } = null!;
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    
    protected readonly VerifyResearcherUseCase.Request Request = new();
    protected bool IsBusy { get; set; } = false;
    protected bool IsBusyResend { get; set; } = false;

    protected override async Task OnInitializedAsync() =>
        Request.Email = await LocalStorage.GetItemAsync<string>("email") ?? string.Empty;

    protected async Task VerifyAsync()
    {
        try
        {
            IsBusy = true;

            var response = await AccountContextHandler.VerifyResearcherAsync(Request);

            if (response is not null)
                if (response.IsSuccessful)
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
        finally
        {
            IsBusy = false;
        }
    }

    protected async Task ResendVerificationCodeAsync()
    {
        ResendVerificationCodeResearcherUseCase.Request request = new(Request.Email);

        try
        {
            IsBusyResend = true;

            var response = await AccountContextHandler.ResendVerificationCodeResearcherAsync(request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Snackbar.Add(response.Data.Message, Severity.Success);
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
        finally
        {
            IsBusyResend = false;
        }
    }
}