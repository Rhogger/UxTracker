using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Web.Components.Dialogs;
using UxTracker.Core.Contexts.Account.Handlers;
using GetResearcherUseCase = UxTracker.Core.Contexts.Account.UseCases.GetResearcher;
using UpdateResearcherUseCase = UxTracker.Core.Contexts.Account.UseCases.UpdateResearcher;

namespace UxTracker.Web.Pages.Contexts.Account.UseCases.GetResearcher;

public class Profile : ComponentBase
{
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] protected IDialogService DialogService { get; set; } = null!;

    protected GetResearcherUseCase.Response? Response { get; set; }
    protected UpdateResearcherUseCase.Request Request { get; set; } = new();
    protected string DeleteRequest { get; set; } = string.Empty;

    protected string ConfirmPassword { get; set; } = string.Empty;
    
    protected bool IsBusy = true;
    protected bool IsBusyUpdate;
    protected bool IsEditState;

    protected string Link = string.Empty;
    private const string ImageSize = "288";

    protected override async Task OnInitializedAsync() => await GetUserAsync();

    private async Task GetUserAsync()
    {
        try
        {
            var response = await AccountContextHandler.GetResearcherAsync();

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Response = response.Data!;
                    Request.Id = Response.Data?.Id;
                    Request.Name = Response.Data?.Name;
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
            IsBusyUpdate = true;
            
            var response = await AccountContextHandler.UpdateResearcherAsync(Request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    var message = response.Data!.Message;
                    if (message != null) Snackbar.Add(message, Severity.Success);
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
            IsBusyUpdate = false;
            StateHasChanged();
        }
    }

    protected async Task DeleteAccountAsync(string password)
    {
        var parameters = new DialogParameters<DeleteResearchConfirmationDialog> { { x => x.Password, password} };
        var dialog = await DialogService.ShowAsync<DeleteResearchConfirmationDialog>("Delete Researcher", parameters);
        await dialog.Result;
    }

    protected void ChangeState()
    {
        IsEditState = !IsEditState;
    }
    
    protected void NavigateToProjects()
    {
        Navigation.NavigateTo("/projects/");
    }
}