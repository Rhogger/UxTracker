using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using UxTracker.Core.Contexts.Research.Handlers;
using UxTracker.Web.Components.Dialogs;
using GetUseCase = UxTracker.Core.Contexts.Research.UseCases.Get;
using UpdateUseCase = UxTracker.Core.Contexts.Research.UseCases.Update;

namespace UxTracker.Web.Pages.Contexts.Research.UseCases.Project;

public class Project: ComponentBase
{
    [Inject] protected IDialogService DialogService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; }
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;
    [Inject] protected IResearchContextHandler ResearchContextHandler { get; set; } = null!;
    
    [Parameter] public Guid ProjectId { get; set; }

    protected GetUseCase.Response Response { get; set; } = null!;
    protected UpdateUseCase.Request UpdateRequest { get; set; } = null!;
    
    protected bool IsBusy { get; set; } = true;
    protected bool IsEditState = false;
    protected MudTextField<string> CopyTextField = null!;
    
    protected override async Task OnInitializedAsync() => await GetProjectAsync();

    private async Task GetProjectAsync()
    {
        try
        {
            var response = await ResearchContextHandler.GetProjectAsync(ProjectId.ToString());

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Response = response.Data!;
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
    
    protected async void DownloadFileAsync()
    {
        try
        {
            var response = await ResearchContextHandler.GetConsentTermAsync(ProjectId.ToString());
            
            if (!string.IsNullOrEmpty(response))
            {
                await JsRuntime.InvokeVoidAsync("downloadFile", [response, Response.Data.Project.ConsentTermName]);
            }
            else
                Snackbar.Add($"Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"{ex.Message}", Severity.Error);
        }
    }
    
    protected async Task DeleteAsync()
    {
        var parameters = new DialogParameters<DeleteProjectConfirmationDialog>{{x => x.ProjectId, ProjectId.ToString()}};
        var dialog = await DialogService.ShowAsync<DeleteProjectConfirmationDialog>("Delete Project", parameters);
        await dialog.Result;
    }
    
    protected async Task CopyTextToClipboard()
    {
        if (!string.IsNullOrEmpty(CopyTextField.Value))
        {
            var textToCopy = CopyTextField.Value;
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", textToCopy);
            Snackbar.Add("Link copiado para sua área de transferência!", Severity.Normal);
        }
    }
    
    protected void ChangeState()
    {
        // if (!IsEditState)
        // {
        //     OriginalTitle = Response.Data.Project.;
        //     OriginalDescription = Description;
        // }
        // IsEditState = !IsEditState;
    }


    protected void SaveChanges()
    {

        IsEditState = false;

    }
    
    protected void CancelChanges()
    {
        // Title = OriginalTitle;
        // Description = OriginalDescription;
        //
        // IsEditState = false;
    }

    protected async Task FinishResearch()
    {
        Snackbar.Add("Pesquisa finalizada com sucesso!", Severity.Success);
    }
}