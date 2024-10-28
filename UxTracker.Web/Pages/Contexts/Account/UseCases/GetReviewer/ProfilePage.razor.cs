using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Web.Components.Dialogs;
using UxTracker.Core.Contexts.Account.Handlers;
using GetReviewerUseCase = UxTracker.Core.Contexts.Account.UseCases.GetReviewer;
using UpdateReviewerUseCase = UxTracker.Core.Contexts.Account.UseCases.UpdateReviewer;

namespace UxTracker.Web.Pages.Contexts.Account.UseCases.GetReviewer;

public class Profile : ComponentBase
{
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected IDialogService DialogService { get; set; } = null!;
    [Inject] protected ILocalStorageService LocalStorage { get; set; } = null!;

    protected GetReviewerUseCase.Response Response { get; set; } = null!;
    protected UpdateReviewerUseCase.Request Request { get; set; } = new();
    
    protected bool IsBusy = true;
    protected bool IsEditState = false;

    protected string Link = string.Empty;
    private string ImageSize = "288";
    
    protected override async Task OnInitializedAsync() => await GetUserAsync();

    private async Task GetUserAsync()
    {
        try
        {
            var response = await AccountContextHandler.GetReviewerAsync();

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Response = response.Data!;
                    Request.Id = Response.Data.Id;
                    Request.Country = Response.Data.Country;
                    Request.State = Response.Data.State;
                    Request.City = Response.Data.City;
                    Link = $"https://gravatar.com/avatar/{Response.Data!.Hash}?s={ImageSize}";
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
            StateHasChanged();
        }
    }
    
    protected async Task UpdateAccountAsync()
    {
        try
        {
            var response = await AccountContextHandler.UpdateReviewerAsync(Request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Snackbar.Add(response.Data!.Message, Severity.Success);
                    await GetUserAsync();
                    ChangeState();
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
            StateHasChanged();
        }
    }

    protected async Task DeleteAccountAsync()
    {
        var parameters = new DialogParameters<DeleteReviewerConfirmationDialog>();
        var dialog = await DialogService.ShowAsync<DeleteReviewerConfirmationDialog>("Delete Reviewer", parameters);
        await dialog.Result;
    }

    protected void ChangeState()
    {
        IsEditState = !IsEditState;
    }
    
    protected async void NavigateToResearch()
    {
        var code = await LocalStorage.GetItemAsync<string>("researchCode");
        Navigation.NavigateTo($"/reviewers/research/{code}");
    }
}