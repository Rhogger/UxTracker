using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using AuthenticateReviewerUseCase = UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer;
using UxTracker.Core.Security;

namespace UxTracker.Web.Pages.Contexts.Account.UseCases.AuthenticateReviewer
{
    public class Login : ComponentBase
    {
        [Inject] protected IBlazorAuthenticationStateProvider BlazorAuthenticationStateProvider { get; set; } = null!;
        [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
        [Inject] protected NavigationManager Navigation { get; set; } = null!;
        [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;
        [Inject] protected ISnackbar Snackbar { get; set; } = null!;

        protected readonly AuthenticateReviewerUseCase.Request Request = new();
        protected bool IsBusy { get; set; }
        protected bool IsRequired { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Request.Email = await LocalStorage.GetItemAsync<string>("email") ?? string.Empty;
            Request.ResearchCode = await LocalStorage.GetItemAsync<string>("researchCode") ?? string.Empty;
            await LocalStorage.SetItemAsync("type", "reviewer");
            
            IsRequired = true;
        }

        protected async Task SignInAsync()
        {
            try
            {
                IsBusy = true;

                var response = await AccountContextHandler.SignInReviewerAsync(Request);

                if (response is not null)
                    if (response.IsSuccessful)
                    {
                        BlazorAuthenticationStateProvider.NotifyAuthenticationStateChanged();
                        Navigation.NavigateTo($"/reviewers/research/{response.Data?.Data?.ResearchCode}");
                    }
                    else
                    {
                        if (response.Data!.Notifications is not null)
                            foreach (var notification in response.Data.Notifications)
                                Snackbar.Add(notification.Message, Severity.Error);
                        else
                        {
                            if (string.Equals(response.Data.Message, "Conta inativa"))
                                Snackbar.Add(
                                    $"Erro: {response.Data.StatusCode} - {response.Data.Message}. Por favor, verifique primeiro.",
                                    Severity.Error,
                                    config =>
                                    {
                                        config.Action = "Verificar";
                                        config.ActionColor = Color.Warning;
                                        config.Onclick = _ =>
                                        {
                                            Navigation.NavigateTo("/reviewers/verify");
                                            return Task.CompletedTask;
                                        };
                                    });
                            else
                            {
                                if (response.Data.Message is "Usuário não cadastrado")
                                    Navigation.NavigateTo("/reviewers/register");

                                Snackbar.Add($"Erro: {response.Data.StatusCode} - {response.Data.Message}",
                                    Severity.Error);
                            }
                        }
                    }
                else
                    Snackbar.Add($"Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.", Severity.Error);
            }
            catch (Exception ex)
            {
                Snackbar.Add(ex.Message, Severity.Error);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}