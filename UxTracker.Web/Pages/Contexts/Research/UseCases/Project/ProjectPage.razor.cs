using ApexCharts;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using MudBlazor;
using UxTracker.Core;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Research.Handlers;
using UxTracker.Core.Contexts.Research.ValueObjects;
using UxTracker.Web.Components.Dialogs;
using UxTracker.Web.Components.Relatories;
using Color = MudBlazor.Color;
using GetUseCase = UxTracker.Core.Contexts.Research.UseCases.Get;
using UpdateUseCase = UxTracker.Core.Contexts.Research.UseCases.Update;
using UpdateStatusUseCase = UxTracker.Core.Contexts.Research.UseCases.UpdateStatus;
using UpdateCluster = UxTracker.Core.Contexts.Research.UseCases.UpdateNumberCluster;

namespace UxTracker.Web.Pages.Contexts.Research.UseCases.Project;

public class Project: ComponentBase
{
    [Inject] protected IDialogService DialogService { get; set; } = null!;
    [Inject] private IJSRuntime JsRuntime { get; set; } = null!;
    [Inject] private ISnackbar Snackbar { get; set; } = null!;
    [Inject] private NavigationManager Navigation { get; set; } = null!;
    [Inject] protected IResearchContextHandler ResearchContextHandler { get; set; } = null!;
    
    [Parameter] public Guid ProjectId { get; set; }

    protected GetUseCase.Response Response { get; set; } = null!;
    private UpdateUseCase.Response? UpdateResponse { get; set; }
    protected UpdateUseCase.Request UpdateRequest { get; set; } = new();
    private UpdateStatusUseCase.Request UpdateStatusRequest { get; set; } = new();
    protected UpdateCluster.Request UpdateClusterRequest = new();

    private List<GetRelatoriesDto>? Relatories { get; set; }= new();
    protected List<SelectedRelatories> SelectedRelatories { get; set;} = new();

    protected bool IsBusy { get; private set; } = true;
    protected bool IsBusyUpdate { get; private set; } = false;
    protected bool IsBusyDelete { get; private set; } = false;
    protected bool IsRelatoriesBusy { get; private set; } = true;
    protected bool IsChangeStatusBusy { get; private set; } = false;
    protected bool IsBusyCluster { get; set; } = false;

    protected bool IsEditState;
    protected Color ColorButtonChangeStatus { get; private set; } = Color.Default;
    protected string TextChangeStatusButton { get; private set; } = string.Empty;
    private const string DefaultDragClass = "d-flex flex-column justify-center align-center relative rounded-lg border-2 border-dashed w-full h-full";
    protected string DragClass = DefaultDragClass;
    protected string? FileName = string.Empty;
    private IBrowserFile? _consentTerm;
    protected MudTextField<string> CopyTextField = null!;
    protected ClustersChart? ClustersChartRef;



    
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
                    UpdateRequest.ProjectId = ProjectId.ToString();
                    UpdateStatusRequest.ProjectId = ProjectId.ToString();
                    FileName = Response.Data?.Project.ConsentTermName;
                    if (Response.Data != null)
                    {
                        UpdateClusterRequest.NumberCluster = Response.Data.Project.ClusterNumber;

                        TextChangeStatusButton = Response.Data.Project.Status switch
                        {
                            Status.NotStarted => "Iniciar Pesquisa",
                            Status.InProgress => "Finalizar Pesquisa",
                            Status.Finished => "Retomar Pesquisa",
                            _ => TextChangeStatusButton
                        };

                        ColorButtonChangeStatus = Response.Data is { Project.Status: Status.InProgress }
                            ? Color.Success
                            : Color.Warning;
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
                Snackbar.Add("Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.", Severity.Error);
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
                    if (response.Data?.Data?.Relatories != null) 
                        Relatories?.AddRange(response.Data?.Data?.Relatories!);

                    if (Relatories != null)
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
                Snackbar.Add("Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.", Severity.Error);
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
            IsBusyUpdate = true;
            
            foreach (var selected in SelectedRelatories.Where(selected => selected.IsChecked))
            {
                UpdateRequest.Relatories.Add(selected.Id.ToString());
            }
            
            var response = await ResearchContextHandler.UpdateProjectAsync(UpdateRequest,_consentTerm);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    UpdateResponse = response.Data;

                    if (Response.Data != null)
                    {
                        Response.Data.Project.Title = UpdateResponse?.Data?.Project.Title;
                        Response.Data.Project.Description = UpdateResponse?.Data?.Project.Description;
                        Response.Data.Project.StartDate = UpdateResponse?.Data?.Project.StartDate;
                        Response.Data.Project.EndDate = UpdateResponse?.Data?.Project.EndDate;
                        if (UpdateResponse is { Data: not null })
                        {
                            Response.Data.Project.PeriodType = UpdateResponse.Data.Project.PeriodType;
                            Response.Data.Project.SurveyCollections = UpdateResponse.Data.Project.SurveyCollections;
                            Response.Data.Project.Status = UpdateResponse.Data.Project.Status;
                            Response.Data.Project.Relatories = UpdateResponse.Data.Project.Relatories;
                        }
                        
                        Response.Data.Project.ConsentTermName = FileName;

                        if (UpdateResponse?.Data?.Project.Relatories != null)
                        {
                            SelectedRelatories = [];
                        
                            foreach (var selected in UpdateResponse.Data.Project.Relatories.Select(relatory =>
                                         new SelectedRelatories
                                         {
                                             Id = relatory.Id,
                                             Title = relatory.Title,
                                             IsChecked = CheckRelatory(relatory.Id.ToString()),
                                         }))
                            {
                                SelectedRelatories.Add(selected);
                            }
                        }

                        TextChangeStatusButton = Response.Data.Project.Status switch
                        {
                            Status.NotStarted => "Iniciar Pesquisa",
                            Status.InProgress => "Finalizar Pesquisa",
                            Status.Finished => "Retomar Pesquisa",
                            _ => TextChangeStatusButton
                        };
                        
                        ColorButtonChangeStatus = Response.Data.Project.Status.Equals(Status.InProgress)
                            ? Color.Success
                            : Color.Warning;
                    }

                    ChangeState();
                    
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
                Snackbar.Add("Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.", Severity.Error);
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
    
    protected async Task DeleteAsync()
    {
        if (ProjectId.ToString().Equals("521dd4e8-82cc-4668-abf3-7ab14b3906da"))
        {
            Snackbar.Add("Essa pesquisa não pode ser excluída!", Severity.Error);
        }
        else
        {
            var parameters = new DialogParameters<DeleteProjectConfirmationDialog>{
                {x => x.ProjectId, ProjectId.ToString()},
                {x => x.IsBusy, IsBusyDelete}};
            var dialog = await DialogService.ShowAsync<DeleteProjectConfirmationDialog>("Delete Project", parameters);
            await dialog.Result;
        }
    }
    
    private async Task UpdateStatusAsync()
    {
        try
        {
            IsChangeStatusBusy = true; 
            
            var response = await ResearchContextHandler.UpdateStatusAsync(UpdateStatusRequest);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    if (Response.Data != null)
                    {
                        Response.Data.Project.StartDate = response.Data?.Data?.Project.StartDate;
                        Response.Data.Project.EndDate = response.Data?.Data?.Project.EndDate;
                        if (response.Data?.Data != null)
                            Response.Data.Project.Status = response.Data.Data.Project.Status;
                        TextChangeStatusButton = Response.Data.Project.Status switch
                        {
                            Status.NotStarted => "Iniciar Pesquisa",
                            Status.InProgress => "Finalizar Pesquisa",
                            Status.Finished => "Retomar Pesquisa",
                            _ => TextChangeStatusButton
                        };
                        ColorButtonChangeStatus = Response.Data.Project.Status.Equals(Status.InProgress)
                            ? Color.Success
                            : Color.Warning;
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
                Snackbar.Add("Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"{ex.Message}", Severity.Error);
        }
        finally
        {
            IsChangeStatusBusy = false;
            StateHasChanged();
        }
    }
    
    protected async Task UpdateClusterNumber()
    {
        try
        {
            IsBusyCluster = true;
            UpdateClusterRequest.ProjectId = ProjectId.ToString();
            
            var response = await ResearchContextHandler.UpdateNumberClusterAsync(UpdateClusterRequest);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Snackbar.Add("Quantidade de clusters alterada com sucesso", Severity.Success);
                    if (Response.Data != null) Response.Data.Project.ClusterNumber = UpdateClusterRequest.NumberCluster;
                    if (ClustersChartRef != null) await ClustersChartRef.RecalculateAndRender();
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
                Snackbar.Add("Ocorreu algum erro no nosso servidor. Por favor, tente mais tarde.", Severity.Error);
        }
        catch (Exception ex)
        {
            Snackbar.Add($"{ex.Message}", Severity.Error);
        }
        finally
        {
            IsBusyCluster = false;
            StateHasChanged();
        }
    }
    
    protected async Task ChangeStatusAsync()
    {
        UpdateStatusRequest.StartDate = Response.Data is { Project.Status: Status.NotStarted } ? DateTime.UtcNow : Response.Data?.Project.StartDate;

        UpdateStatusRequest.EndDate = Response.Data switch
        {
            { Project.Status: Status.InProgress } => DateTime.UtcNow,
            { Project.Status: Status.Finished } => null,
            _ => Response.Data?.Project.EndDate
        };

        await UpdateStatusAsync();
    }
    
    protected async void DownloadFileAsync()
    {
        try
        {
            await ResearchContextHandler.DownloadConsentTermAsync(ProjectId.ToString(), FileName, JsRuntime);
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
        SetUpdateValues();

        if (!IsEditState)
        {
            FileName = Response.Data?.Project.ConsentTermName;
            _consentTerm = null;
            SelectedRelatories = [];
            if (Relatories != null)
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
            
        IsEditState = !IsEditState;

        if (Relatories is { Count: 0 })
            await GetRelatoriesAsync();
    }

    private bool CheckRelatory(string id) => 
        Response.Data?.Project.Relatories != null &&
        Response.Data != null && 
        Response.Data.Project.Relatories
            .Any(relatory => relatory.Id.ToString() == id);

    private void SetUpdateValues()
    {
        UpdateRequest.Title = Response.Data?.Project.Title;
        UpdateRequest.Description = Response.Data?.Project.Description;
        UpdateRequest.StartDate = Response.Data?.Project.StartDate;
        UpdateRequest.EndDate = Response.Data?.Project.EndDate;
        if (Response.Data == null) return;
        UpdateRequest.PeriodType = Response.Data.Project.PeriodType;
        UpdateRequest.SurveyCollections = Response.Data.Project.SurveyCollections;
    }
   

    protected void OnInputFileChanged(InputFileChangeEventArgs e)
    {
        ClearDragClass();
        var file = e.File;

        if (file.ContentType == "application/pdf")
        {
            if (file.Size > Configuration.ConsentTerm.MaxSize)
            {
                Snackbar.Add($"O tamanho máximo suportado é 2MB.", Severity.Error);
            }
            else
            {
                FileName = file.Name;
                _consentTerm = file;
            }
        }
        else
        {
            Snackbar.Add($"Arquivo '{file.Name}' não é um PDF.", Severity.Error);
        }
    }

    protected void NavigateToBack()
    {
        if (!IsEditState)
            Navigation.NavigateTo("/projects/");
        else
            ChangeState();
    }

    protected void SetDragClass()
        => DragClass = $"{DefaultDragClass} mud-border-primary";

    protected void ClearDragClass()
        => DragClass = DefaultDragClass;
}