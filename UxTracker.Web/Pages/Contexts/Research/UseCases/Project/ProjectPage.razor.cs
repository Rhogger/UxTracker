using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.Handlers;
using UxTracker.Core.Contexts.Research.ValueObjects;
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
    protected UpdateUseCase.Response UpdateResponse { get; set; } = null!;
    protected UpdateUseCase.Request UpdateRequest { get; set; } = new();
    protected List<GetRelatoriesDTO> Relatories { get; set; }= new();
    protected List<SelectedRelatories> SelectedRelatories { get; set;} = new();

    protected bool IsBusy { get; set; } = true;
    protected bool IsRelatoriesBusy { get; set; } = true;
    protected bool IsEditState = false;
    protected bool IsValid = true;
    protected const string DefaultDragClass = "d-flex flex-column justify-center align-center relative rounded-lg border-2 border-dashed w-full h-full";
    protected string DragClass = DefaultDragClass;
    protected string FileName = string.Empty;
    protected IBrowserFile? ConsentTerm = null;
    protected byte[]? ConsentTermBytes { get; set; } = null!;
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
                    FileName = Response.Data.Project.ConsentTermName;
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
    
    private async Task GetRelatoriesAsync()
    {
        try
        {
            var response = await ResearchContextHandler.GetRelatoriesAsync();

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Relatories.AddRange(response.Data.Data.Relatories);

                    foreach (var selected in Relatories.Select(relatory => new SelectedRelatories
                             {
                                 Id = relatory.Id,
                                 Title = relatory.Title,
                                 IsChecked = CheckRelatory(relatory.Id.ToString()),
                             }))
                    {
                        SelectedRelatories.Add(selected);
                    }
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
            IsRelatoriesBusy = false;
            StateHasChanged();
        }
    }
    
    protected async Task UpdateAsync()
    {
        try
        {
            foreach (var selected in SelectedRelatories.Where(selected => selected.IsChecked))
            {
                UpdateRequest.Relatories.Add(selected.Id.ToString());
            }
            
            var response = await ResearchContextHandler.UpdateProjectAsync(UpdateRequest,ConsentTerm);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    UpdateResponse = response.Data;
                    
                    Response.Data.Project.Title = UpdateResponse.Data.Project.Title;
                    Response.Data.Project.Description = UpdateResponse.Data.Project.Description;
                    Response.Data.Project.StartDate = UpdateResponse.Data.Project.StartDate;
                    Response.Data.Project.EndDate = UpdateResponse.Data.Project.EndDate;
                    Response.Data.Project.PeriodType = UpdateResponse.Data.Project.PeriodType;
                    Response.Data.Project.SurveyCollections = UpdateResponse.Data.Project.SurveyCollections;
                    Response.Data.Project.Status = UpdateResponse.Data.Project.Status;
                    Response.Data.Project.Relatories = UpdateResponse.Data.Project.Relatories;
                    Response.Data.Project.ConsentTermName = FileName;
                    
                    IsEditState = !IsEditState;
                    Snackbar.Add("Projeto atualizado com sucesso", Severity.Success);
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
    
    protected async Task DeleteAsync()
    {
        var parameters = new DialogParameters<DeleteProjectConfirmationDialog>{{x => x.ProjectId, ProjectId.ToString()}};
        var dialog = await DialogService.ShowAsync<DeleteProjectConfirmationDialog>("Delete Project", parameters);
        await dialog.Result;
    }
    
    protected async void DownloadFileAsync()
    {
        try
        {
            await ResearchContextHandler.GetConsentTermAsync(ProjectId.ToString(), FileName, JsRuntime);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"{ex.Message}", Severity.Error);
        }
    }
    
    protected async Task CopyTextToClipboard()
    {
        if (!string.IsNullOrEmpty(CopyTextField.Value))
        {
            var textToCopy = CopyTextField.Value;
            await JsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", textToCopy);
            Snackbar.Add("Link copiado para sua área de transferência!");
        }
    }
    
    protected async void ChangeState()
    {
        if (!IsEditState)
        {
            UpdateRequest.ProjectId = ProjectId.ToString();
            UpdateRequest.Title = Response.Data.Project.Title;
            UpdateRequest.Description = Response.Data.Project.Description;
            UpdateRequest.StartDate = Response.Data.Project.StartDate;
            UpdateRequest.EndDate = Response.Data.Project.EndDate;
            UpdateRequest.PeriodType = Response.Data.Project.PeriodType;
            UpdateRequest.SurveyCollections = Response.Data.Project.SurveyCollections;
        }
        
        IsEditState = !IsEditState;

        if (Relatories.Count == 0)
        {
            await GetRelatoriesAsync();
            
            
        }
    }

    protected bool CheckRelatory(string id)
    {
        return Response.Data.Project.Relatories.Any(relatory => relatory.Id.ToString() == id);
    }
   

    protected void OnInputFileChanged(InputFileChangeEventArgs e)
    {
        ClearDragClass();
        var file = e.File;

        if (file.ContentType == "application/pdf")
        {
            FileName = file.Name;
            ConsentTerm = file;
        }
        else
        {
            Snackbar.Add($"Arquivo '{file.Name}' não é um PDF.", Severity.Error);
        }
    }

    protected void SetDragClass()
        => DragClass = $"{DefaultDragClass} mud-border-primary";

    protected void ClearDragClass()
        => DragClass = DefaultDragClass;
}