using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.Handlers;
using UxTracker.Core.Contexts.Research.Validators;
using CreateUseCase = UxTracker.Core.Contexts.Research.UseCases.Create;

namespace UxTracker.Web.Pages.Contexts.Research.UseCases.Create;

public class NewProject: ComponentBase
{
    [Inject] protected IResearchContextHandler ResearchContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    
    protected List<GetRelatoriesDTO> Relatories = new();
    protected CreateUseCase.Request Request = new();

    protected bool IsBusy { get; set; } = true;
    protected MudFileUpload<IBrowserFile>? FileUpload;
    protected IBrowserFile AcceptTerm = null!;
    protected string? FileName;
    protected const string DefaultDragClass = "d-flex flex-column justify-center align-center relative rounded-lg border-2 border-dashed w-full h-full";
    protected string DragClass = DefaultDragClass;
    protected bool IsValid = true;

    protected override async Task OnInitializedAsync() => await GetRelatoriesAsync();

    protected async Task CreateProjectAsync()
    {
        try
        {
            var response = await ResearchContextHandler.CreateProjectAsync(Request, AcceptTerm);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Snackbar.Add(response.Data!.Message, Severity.Success);
                    Navigation.NavigateTo($"/project/{response.Data.Data.Id}");
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
    
    protected async Task GetRelatoriesAsync()
    {
        try
        {
            var response = await ResearchContextHandler.GetRelatoriesAsync();

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Relatories.AddRange(response.Data.Data.Relatories);
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
    
    protected void NavigateToProjects()
    {
        Navigation.NavigateTo("/projects/");
    }

    protected async Task ClearAsync()
    {
        await (FileUpload?.ClearAsync() ?? Task.CompletedTask);
        FileName = null;
        ClearDragClass();
    }

    protected void OnInputFileChanged(InputFileChangeEventArgs e)
    {
        ClearDragClass();
        var file = e.File;

        if (file.ContentType == "application/pdf")
        {
            FileName = file.Name;
            AcceptTerm = file;
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

    protected void AddRelatoryOnList(bool isChecked, string relatoryId)
    {
        if (isChecked)
        {
            if (!Request.Relatories.Contains(relatoryId))
                Request.Relatories.Add(relatoryId);
        }
        else
        {
            if (Request.Relatories.Contains(relatoryId))
                Request.Relatories.Remove(relatoryId);
        }

        if (RelatoriesValidator.Validate(Request.Relatories) is not null)
        {       
            IsValid = false;
        }
        else
        {
            IsValid = true;   
        }
        
        StateHasChanged();
    }
}