using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Research.Handlers;
using UxTracker.Core.Contexts.Review.Handlers;
using UxTracker.Core.Contexts.Review.ValueObjects;
using UxTracker.Web.Components.Dialogs;
using GetForReviewUseCase = UxTracker.Core.Contexts.Research.UseCases.GetForReview;
using RatingUseCase = UxTracker.Core.Contexts.Review.UseCases.Rating;

namespace UxTracker.Web.Pages.Contexts.Reviewer.UseCases.Review;

public class Review: ComponentBase
{
    [Inject] protected IAccountContextHandler AccountContextHandler { get; set; } = null!;
    [Inject] protected IResearchContextHandler ResearchContextHandler { get; set; } = null!;
    [Inject] protected IReviewContextHandler ReviewContextHandler { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;
    [Inject] private IDialogService DialogService { get; set; } =null!;

    [Parameter] public Guid ProjectId { get; set; }

    protected RatingUseCase.Request RatingRequest { get; set; } = new();
    protected GetForReviewUseCase.Response Response { get; set; } = null!;

    protected bool IsBusy { get; private set; } = true;
    protected bool IsBusyRate { get; set; }
    protected bool IsDisabled { get; set; } = true;
    protected bool IsDisabledCountdown { get; private set; } = true;
    private readonly System.Timers.Timer _timer = new(1000);
    protected int Days, Hours, Minutes, Seconds;
    private DateTime? _comingSoonDate;

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

                    if (Response.Data != null && Response.Data.Project!.Status.Equals(Status.NotStarted))
                    {
                        Snackbar.Add("A Pesquisa ainda não iniciou", Severity.Error);
                        await AccountContextHandler.SignOutAsync();
                    }
                    
                    if (Response.Data is { Accepted: false })
                        await OpenDialogAsync();

                    // if (Response.Data?.Project is { Reviews.Count: > 0 })
                    // {
                    //     if (Response.Data.Project.Reviews.Count < Response.Data.Project.SurveyCollections)
                    //     {
                    //         IsDisabled = false;
                    //     }
                    //
                    //     if (!UserRates.ValidToRate(
                    //             Response.Data.Project.PeriodType,
                    //             Response.Data.Project.Reviews.Last().RatedAt))
                    //     {
                    //         StartCountDown();
                    //         IsDisabled = true;
                    //     }
                    // }
                    // else
                    //     IsDisabled = false;
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
    
    protected async Task RatingAsync()
    {
        try
        {
            IsBusyRate = true;

            RatingRequest.ProjectId = ProjectId.ToString();
            
            var response = await ReviewContextHandler.RatingAsync(RatingRequest);

            if (response is not null)
                if (response.IsSuccessful)
                {
                    IsDisabled = true;
                    
                    Snackbar.Add("Avaliação enviada", Severity.Success);

                    UserRates rate = new();

                    if (response.Data?.Data != null)
                    {
                        rate.Index = response.Data.Data.Rate.Index;
                        rate.Rate = response.Data.Data.Rate.Rate;
                        rate.Comment = response.Data.Data.Rate.Comment;
                        rate.RatedAt = response.Data.Data.Rate.RatedAt;
                    }

                    Response.Data?.Project?.Reviews.Add(rate);

                    // StartCountDown();
                    
                    StateHasChanged();
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
            IsBusyRate = false;
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
            { nameof(ShowAcceptTerm.TermUrl), Response.Data?.TermUrl },
            { nameof(ShowAcceptTerm.ProjectId), ProjectId }
        };
        
        return DialogService.ShowAsync<ShowAcceptTerm>("Termo de Consentimento Livre e Esclarecido", parameters, options);
    }

    private void CountDown()
    {
        var distance = _comingSoonDate - DateTime.Now;
        
        if (distance != null)
        {
            Days = distance.Value.Days;
            Hours = distance.Value.Hours;
            Minutes = distance.Value.Minutes;
            Seconds = distance.Value.Seconds;
        }

        if (Days <= 0 && Hours <= 0 && Minutes <= 0 && Seconds <= 0)
        {
            Days = 0;
            Hours = 0;
            Minutes = 0;
            Seconds = 0;
            _timer.Stop();
        }

        InvokeAsync(StateHasChanged);
    }

    private void StartCountDown()
    {
        IsDisabledCountdown = false;
        if (Response.Data != null)
            _comingSoonDate = UserRates.GetComingSoonDate(
                Response.Data?.Project?.PeriodType,
                Response.Data?.Project!.Reviews.Last().RatedAt);
        _timer.Elapsed += (_, _) => CountDown();
        _timer.Start();
    }
}