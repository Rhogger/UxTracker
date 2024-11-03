using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Security;
using VerifyReviewerUseCase = UxTracker.Core.Contexts.Account.UseCases.VerifyReviewer;
using AuthenticateReviewerUseCase = UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer;
using ResendVerificationCodeReviewerUseCase = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeReviewer;

namespace UxTracker.Web.Pages.Contexts.Account.UseCases.VerifyReviewer;

public class AccountVerification: ComponentBase
{
    [Inject] protected IBlazorAuthenticationStateProvider BlazorAuthenticationStateProvider { get; set; } = null!;
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;

    protected readonly VerifyReviewerUseCase.Request Request = new();
    protected readonly AuthenticateReviewerUseCase.Request AuthenticateRequest = new();
    protected bool IsBusy { get; set; }
    protected bool IsBusyResend { get; private set; }

    protected override async Task OnInitializedAsync() =>
        Request.Email = await LocalStorage.GetItemAsync<string>("email") ?? string.Empty;

    protected async Task VerifyAsync()
    {
        try
        {
            IsBusy = true;

            Request.ResearchCode = await LocalStorage.GetItemAsync<string>("researchCode") ?? string.Empty;

            var response = await AccountContextHandler.VerifyReviewerAsync(Request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    BlazorAuthenticationStateProvider.NotifyAuthenticationStateChanged();
                    
                    Snackbar.Add("Conta verificada com sucesso!", Severity.Success);
                    
                    Navigation.NavigateTo(string.IsNullOrEmpty(response.Data?.Data?.ResearchCode)
                        ? "reviewers/login"
                        : $"reviewers/research/{response.Data.Data.ResearchCode}");
                }
                else
                {
                    if (response.Data?.Notifications is not null)
                        foreach (var notification in response.Data.Notifications)
                            Snackbar.Add(notification.Message, Severity.Error);
                    else
                    {
                        if (response.Data?.Message != null)
                        {
                            Snackbar.Add($"Erro: {response.Data.StatusCode} - {response.Data.Message}", Severity.Error);
                         
                            if(response.Data.Message.Equals("Código de pesquisa não informado") ||
                               response.Data.Message.Equals("Pesquisa não encontrada"))
                            {
                                Snackbar.Add("Conta verificada com sucesso!", Severity.Success);
                                
                                Navigation.NavigateTo("reviewers/login");
                            }
                        }
                    }
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
        ResendVerificationCodeReviewerUseCase.Request request = new(Request.Email);

        try
        {
            IsBusyResend = true;

            var response = await AccountContextHandler.ResendVerificationCodeReviewerAsync(request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    if (response.Data?.Message != null) Snackbar.Add(response.Data?.Message!, Severity.Success);
                }
                else
                {
                    if (response.Data?.Notifications is not null)
                        foreach (var notification in response.Data.Notifications)
                            Snackbar.Add(notification.Message, Severity.Error);
                    else if (response.Data != null)
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
    
    protected void NavigateToLogin() => Navigation.NavigateTo("/reviewers/login");
}