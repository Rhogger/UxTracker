using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using CreateUseCase = UxTracker.Core.Contexts.Account.UseCases.CreateResearcher;

namespace UxTracker.Web.Pages.Contexts.Account.UseCases.CreateResearcher;

public class Register : ComponentBase
{
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;

    protected readonly CreateUseCase.Request Request = new();
    
    protected string ConfirmPassword = string.Empty;
    protected bool IsBusy;

    protected async Task SignUpAsync()
    {
        try
        {
            IsBusy = true;

            var response = await AccountContextHandler.SignUpResearchAsync(Request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    var message = response.Data!.Message;
                    if (message != null) Snackbar.Add(message, Severity.Success);
                    Navigation.NavigateTo("/verify");
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
        finally
        {
            IsBusy = false;
        }
    }
}