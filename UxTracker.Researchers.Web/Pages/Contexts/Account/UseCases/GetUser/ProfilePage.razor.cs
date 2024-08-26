using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using GetUserUseCase = UxTracker.Core.Contexts.Account.UseCases.GetUser;
using UpdateAccountUseCase = UxTracker.Core.Contexts.Account.UseCases.UpdateAccount;


namespace UxTracker.Researchers.Web.Pages.Contexts.Account.UseCases.GetUser;

public class Profile : ComponentBase
{
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;

    protected GetUserUseCase.Response Response { get; set; } = null!;
    protected UpdateAccountUseCase.Request Request { get; set; } = new();

    protected string ConfirmPassword { get; set; } = string.Empty;
    
    protected bool IsBusy = true;
    protected bool IsEditState = false;

    protected string Link = string.Empty;
    private string ImageSize = "288";
    
    protected override async Task OnInitializedAsync() => await GetUserAsync();

    private async Task GetUserAsync()
    {
        try
        {
            var response = await AccountContextHandler.GetUserAsync();

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Response = response.Data!;
                    Request.Id = Response.Data.Id;
                    Request.Name = Response.Data.Name;
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
            var response = await AccountContextHandler.UpdateAccountAsync(Request);

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

    protected void ChangeState()
    {
        IsEditState = !IsEditState;
    }
}