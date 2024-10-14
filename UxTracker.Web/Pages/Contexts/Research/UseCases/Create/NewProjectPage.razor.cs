using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using MudBlazor;
using UxTracker.Core.Contexts.Research.Handlers;
using CreateUseCase = UxTracker.Core.Contexts.Research.UseCases.Create;

namespace UxTracker.Web.Pages.Contexts.Research.UseCases.Create;

public class NewProject: ComponentBase
{
    [Inject] protected IResearchContextHandler ResearchContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    
    protected CreateUseCase.Request Request = new();

    protected IList<IBrowserFile> files = new List<IBrowserFile>();
    protected string Period { get; set; }

    protected void UploadFiles2(IBrowserFile file)
    {
        files.Add(file);
    }

    protected async Task CreateProjectAsync()
    {
        try
        {
            var response = await ResearchContextHandler.CreateProject(Request);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Snackbar.Add(response.Data!.Message, Severity.Success);
                    Navigation.NavigateTo("/projects/project");
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
    
    protected void NavigateToPreviousPage()
    {
        Navigation.NavigateTo("/projects");
    }

    protected void OnDateChanged() => StateHasChanged();
}