using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Research.Handlers;
using UxTracker.Core.Contexts.Review.ValueObjects;
using UxTracker.Web.Components.Dialogs;
using GetForReviewUseCase = UxTracker.Core.Contexts.Research.UseCases.GetForReview;
using RatingUseCase = UxTracker.Core.Contexts.Review.UseCases.Rating;
using AcceptTermUseCase = UxTracker.Core.Contexts.Review.UseCases.AcceptTerm;

namespace UxTracker.Web.Pages.Contexts.Reviewer.UseCases.Review;

public class Review: ComponentBase
{
    [Inject] protected IResearchContextHandler ResearchContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } =null!;

    [Parameter] public Guid ProjectId { get; set; }
    
    protected RatingUseCase.Request RatingRequest { get; set; } = new();
    protected AcceptTermUseCase.Request AcceptTermRequest { get; set; } = new();
    protected GetForReviewUseCase.Response Response { get; set; } = null!;

    protected bool IsBusy { get; set; } = true;

    protected override async Task OnInitializedAsync() => await GetProjectForReviewAsync();
    
    private async Task GetProjectForReviewAsync()
    {
        try
        {
            var response = await ResearchContextHandler.GetProjectForReviewAsync(ProjectId.ToString());

            if (response is not null)
                if (response.IsSuccessful)
                {
                    Response = response.Data!;

                    if (!Response.Data.Accepted)
                        await OpenDialogAsync();
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

    private Task OpenDialogAsync()
    {
        var options = new DialogOptions
        {
            CloseOnEscapeKey = false, 
            CloseButton = false, 
            BackdropClick = false, 
            FullWidth = true,
            MaxWidth = MaxWidth.Large, 
        };
        
        var parameters = new DialogParameters
        {
            { nameof(ShowAcceptTerm.TermUrl), Response.Data.TermUrl },
            { nameof(ShowAcceptTerm.ProjectId), ProjectId }
        };
        
        return DialogService.ShowAsync<ShowAcceptTerm>("Termo de Consentimento Livre e Esclarecido", parameters, options);
    }

    protected void HandleSubmit()
    {
        // var currentDay = Evaluations.Count + 1;
        // if (Evaluations.Any(e => e.Day == currentDay))
        // {
        //     return;
        // }
        //
        // Evaluations.Add(new EvaluationData { Day = currentDay, Rating = SelectedRating, Comment = Comment });
        // SelectedRating = 0;
        // Comment = string.Empty;
    }
}