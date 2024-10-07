using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Web.Handlers;
using CreateUseCase = UxTracker.Core.Contexts.Account.UseCases.CreateReviewer;

namespace UxTracker.Web.Pages.Contexts.Account.UseCases.CreateReviewer
{
    public class Register : ComponentBase
    {
        [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
        [Inject] protected NavigationManager Navigation { get; set; } = null!;
        [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;
        [Inject] protected ISnackbar Snackbar { get; set; } = null!;

        protected CreateUseCase.Request Request { get; set; } = new();
        
        protected override async Task OnInitializedAsync() => Request.Email = await LocalStorage.GetItemAsync<string>("email") ?? string.Empty;

        protected async Task SignUpAsync()
        {
            try
            {
                var response = await AccountContextHandler.SignUpReviewerAsync(Request);

                if (response is not null)
                    if (response.IsSuccessful)
                    {
                        Snackbar.Add(response.Data!.Message, Severity.Success);
                        Navigation.NavigateTo("reviewers/verify");
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
                Snackbar.Add(ex.Message, Severity.Error);
            }
        }

        protected void Back()
        {
            Navigation.NavigateTo("/reviewers/login/");
        }
    }
}