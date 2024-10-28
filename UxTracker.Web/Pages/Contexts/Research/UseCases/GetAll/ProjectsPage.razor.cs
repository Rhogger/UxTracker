using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Research.Handlers;
using GetAllUseCase = UxTracker.Core.Contexts.Research.UseCases.GetAll;

namespace UxTracker.Web.Pages.Contexts.Research.UseCases.GetAll;

public class Projects : ComponentBase
{
    [Inject] protected IResearchContextHandler ResearchContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    
    protected GetAllUseCase.Response Response { get; private set; } = null!;

    protected bool IsBusy { get; private set; } = true;
    
    protected override async Task OnInitializedAsync() => await GetProjectForReviewAsync();
    
    private async Task GetProjectForReviewAsync()
    {
        try
        {
            var response = await ResearchContextHandler.GetProjectsAsync();

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
    
    protected Color GetStatusColor(Status status)
    {
        return status switch
        {
            Status.NotStarted => Color.Default,
            Status.InProgress => Color.Warning,
            Status.Finished => Color.Success,
            _ => Color.Default
        };
    }

    protected void AddNewProject() =>
        Navigation.NavigateTo("/projects/new");
    
    protected void NavigateToProject(string projectId) =>
        Navigation.NavigateTo($"/project/{projectId}");
    
}