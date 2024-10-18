using ApexCharts;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using UxTracker.Core.Contexts.Research.Handlers;
using UxTracker.Web.Components.Dialogs;
using GetForReviewUseCase = UxTracker.Core.Contexts.Research.UseCases.GetForReview;

namespace UxTracker.Web.Pages.Contexts.Reviewer.UseCases.Review;

public class Review: ComponentBase
{
    [Inject] protected IResearchContextHandler ResearchContextHandler { get; set; } = null!;
    [Inject] protected NavigationManager Navigation { get; set; } = null!;
    [Inject] protected ISnackbar Snackbar { get; set; } = null!;

    [Parameter] public Guid ProjectId { get; set; }
    
    protected List<EvaluationData> Evaluations { get; set; } = new();
    protected int SelectedRating { get; set; }
    protected ApexChartOptions<EvaluationData> options;
    protected string Comment { get; set; }
    private int Height = 400;
    private int Width = 800;
    private IDialogService DialogService;
    
    protected GetForReviewUseCase.Response Response { get; set; } = null!;

    protected bool IsBusy { get; set; } = true;

    
    protected Task OpenDialogAsync()
    {
        var options = new DialogOptions { CloseOnEscapeKey = true, MaxWidth = MaxWidth.Large, FullWidth = true };
        return DialogService.ShowAsync<ShowAcceptTerm>("Termo de Aceite", options);
    }

    protected override void OnInitialized()
    {
        options = new ApexChartOptions<EvaluationData>
        {
            Chart = new Chart
            {
                Height = Height,
                Width = Width,
                Toolbar = new Toolbar
                {
                    Show = false
                },
                DropShadow = new DropShadow
                {
                    Enabled = true,
                    Color = "",
                    Top = 18,
                    Left = 7,
                    Blur = 10,
                    Opacity = 0.2d
                }
            },

            Theme = new Theme
            {
                Mode = Mode.Dark,
                Palette = PaletteType.Palette1
            },

            DataLabels = new ApexCharts.DataLabels
            {
                OffsetY = -6d
            },
            Grid = new Grid
            {
                BorderColor = "#e7e7e7",
                Row = new GridRow
                {
                    Colors = new List<string> { "#f3f3f3", "transparent" },
                    Opacity = 0.5d
                }
            },

            Colors = new List<string> { "#77B6EA", "#545454" },
            Markers = new Markers { Shape = ShapeEnum.Circle, Size = 5, FillOpacity = new Opacity(0.8d) },
            Stroke = new Stroke { Curve = Curve.Smooth },
            Legend = new Legend
            {
                Position = LegendPosition.Top,
                HorizontalAlign = ApexCharts.Align.Right,
                Floating = true,
                OffsetX = -5,
                OffsetY = -25
            },

        };

        Evaluations.Add(new EvaluationData { Day = 1, Rating = 8, Comment = "Nota de exemplo para o dia 1" });
        Evaluations.Add(new EvaluationData { Day = 2, Rating = 6, Comment = "Nota de exemplo para o dia 2" });
        Evaluations.Add(new EvaluationData { Day = 3, Rating = 9, Comment = "Nota de exemplo para o dia 3" });
        Evaluations.Add(new EvaluationData { Day = 4, Rating = 7, Comment = "Nota de exemplo para o dia 4" });
        Evaluations.Add(new EvaluationData { Day = 5, Rating = 10, Comment = "Nota de exemplo para o dia 5" });
        Evaluations.Add(new EvaluationData { Day = 6, Rating = 1, Comment = "Nota de exemplo para o dia 6" });
    }

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
    protected void HandleSubmit()
    {
        var currentDay = Evaluations.Count + 1;
        if (Evaluations.Any(e => e.Day == currentDay))
        {
            return;
        }

        Evaluations.Add(new EvaluationData { Day = currentDay, Rating = SelectedRating, Comment = Comment });
        SelectedRating = 0;
        Comment = string.Empty;
    }

    protected class EvaluationData
    {
        public int Day { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}