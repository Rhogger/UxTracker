using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using RestSharp;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Account.UseCases.UpdatePassword;

namespace UxTracker.Researchers.Web.Pages.Contexts.Account.UseCases.UpdatePasswordAuthenticated;

public partial class UpdatePasswordPage : ComponentBase
{
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } 
    [Inject] protected ILocalStorageService LocalStorage { get; set; }
    [Inject] protected ISnackbar Snackbar { get; set; }
    
    protected Request Request = new();

    protected override async Task OnInitializedAsync() =>
        Request.Email = await LocalStorage.GetItemAsync<string>("email") ?? string.Empty;
    protected async Task UpdatePasswordAsync()
    {
        try
        {
            var response = await AccountContextHandler.UpdateAccountPasswordAsync(Request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Snackbar.Add(response.Data!.Message, Severity.Success);
                    Navigation.NavigateTo("/projects");
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