using ApexCharts;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor;
using UxTracker.Core.Contexts.Research.Handlers;
using GetUseCase = UxTracker.Core.Contexts.Research.UseCases.Get;
using UpdateUseCase = UxTracker.Core.Contexts.Research.UseCases.Update;

namespace UxTracker.Web.Pages.Contexts.Research.UseCases.Project;

public class Project: ComponentBase
{
    [Inject] IJSRuntime JSRuntime { get; set; } = null!;
    [Inject] ISnackbar Snackbar { get; set; } = null!;
    [Inject] NavigationManager NavigationManager { get; set; } = null!;
    [Inject] protected IResearchContextHandler ResearchContextHandler { get; set; } = null!;
    
    [Parameter] public Guid ProjectId { get; set; }

    protected GetUseCase.Response Response { get; set; } = null!;
    protected UpdateUseCase.Request UpdateRequest { get; set; } = null!;
    protected bool IsBusy { get; set; } = true;
    
    // BoxPlot
    protected List<BoxPlotSample> incidents { get; set; } = SampleData.GetBoxPlotData();
    protected ApexChartOptions<BoxPlotSample> options;
    protected ApexChart<BoxPlotSample> BoxPlot;

    // Bar Charts
    protected List<Order> Orders { get; set; } = SampleData.GetOrders();

    protected ApexChartOptions<Order> barChartOptions;
    protected ApexChart<Order> BarChart;

    // Cluster
    protected List<Cluster> clusters { get; set; } = SampleData.GetClusters();
    protected ApexChartOptions<Cluster> ClustersOptions = new();
    
    public class Cluster
    {
        public decimal DiscountPercentage { get; set; }
        public decimal GrossValue { get; set; }
    }
    
    // Campos de data
    protected DateTime? _startDate { get; set; } = DateTime.Today;
    protected DateTime? _endDate { get; set; } = DateTime.Today;

    // Copiar link para a área de transferência
    protected string evaluationLink = "https://exemplo.com.br";
    protected MudTextField<string> textFieldRef;
    
    //botao download termo
    protected string pdfFilePath = "path/to/your/file.pdf";
    
    public static class SampleData
    {
        public static List<BoxPlotSample> GetBoxPlotData()
        {
            return new List<BoxPlotSample>
            {
                new BoxPlotSample { Name = "A", Min = 10, Q1 = 20, Median = 30, Q3 = 40, Max = 50 },
                new BoxPlotSample { Name = "B", Min = 15, Q1 = 25, Median = 35, Q3 = 45, Max = 55 },
                new BoxPlotSample { Name = "C", Min = 15, Q1 = 25, Median = 35, Q3 = 45, Max = 55 },
                new BoxPlotSample { Name = "D", Min = 15, Q1 = 25, Median = 35, Q3 = 45, Max = 55 },
                new BoxPlotSample { Name = "E", Min = 15, Q1 = 25, Median = 35, Q3 = 45, Max = 55 },
                new BoxPlotSample { Name = "F", Min = 15, Q1 = 25, Median = 35, Q3 = 45, Max = 55 },
                new BoxPlotSample { Name = "G", Min = 15, Q1 = 25, Median = 35, Q3 = 45, Max = 55 },
                new BoxPlotSample { Name = "H", Min = 15, Q1 = 25, Median = 35, Q3 = 45, Max = 55 }
            };
        }

        public static List<Order> GetOrders()
        {
            return new List<Order>
            {
                new Order { Country = "USA", GrossValue = 1000000, NetValue = 850000 },
                new Order { Country = "Canada", GrossValue = 750000, NetValue = 650000 },
                new Order { Country = "UK", GrossValue = 800000, NetValue = 720000 },
                new Order { Country = "Germany", GrossValue = 650000, NetValue = 590000 },
                new Order { Country = "France", GrossValue = 700000, NetValue = 630000 }
            };
        }

        public static List<Cluster> GetClusters()
        {
            return new List<Cluster>
            {
                new Cluster { DiscountPercentage = 5, GrossValue = 3000 },
                new Cluster { DiscountPercentage = 70, GrossValue = 70000 },
                new Cluster { DiscountPercentage = 30, GrossValue = 30000 },
                new Cluster { DiscountPercentage = 50, GrossValue = 50000 },
                new Cluster { DiscountPercentage = 90, GrossValue = 90000 },
                new Cluster { DiscountPercentage = 20, GrossValue = 20000 },
                new Cluster { DiscountPercentage = 80, GrossValue = 80000 },
                new Cluster { DiscountPercentage = 10, GrossValue = 10000 },
                new Cluster { DiscountPercentage = 40, GrossValue = 40000 },
                new Cluster { DiscountPercentage = 60, GrossValue = 60000 },
                new Cluster { DiscountPercentage = 60, GrossValue = 50000 },
                new Cluster { DiscountPercentage = 60, GrossValue = 40000 },
                new Cluster { DiscountPercentage = 5, GrossValue = 5000 },
                new Cluster { DiscountPercentage = 70, GrossValue = 3000 },

            };
        }
    }

    public class BoxPlotSample
    {
        public string Name { get; set; }
        public double Min { get; set; }
        public double Max { get; set; }
        public double Q1 { get; set; }
        public double Q3 { get; set; }
        public double Median { get; set; }
    }

    public class Order
    {
        public string Country { get; set; }
        public decimal GrossValue { get; set; }
        public decimal NetValue { get; set; }
    }
    
    protected string Status = "Em andamento";
    protected bool IsEditState = false;
    private string OriginalTitle;
    private string OriginalDescription;
    
    protected override async Task OnInitializedAsync()
    {
        options = new ApexChartOptions<BoxPlotSample>
        {
            Theme = new Theme
            {
                Mode = Mode.Light,
            }
        };

        barChartOptions = new ApexChartOptions<Order>
        {
            Theme = new Theme
            {
                Mode = Mode.Light,
            }
        };

        ClustersOptions = new ApexChartOptions<Cluster>
        {
            Tooltip = new ApexCharts.Tooltip { Shared = false, Intersect = true },
            Markers = new Markers { Size = 6 },

            Theme = new Theme
            {
                Mode = Mode.Light,
            }
        };
        
        await GetProjectAsync();
    }

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

        Status = "Concluído";

        Snackbar.Add("Pesquisa finalizada com sucesso!", Severity.Success);
    }

    protected async Task UpdateChart()
    {
        await BoxPlot.RenderAsync();
        await BarChart.RenderAsync();
    }

    protected async Task CopyTextToClipboard()
    {
        if (textFieldRef != null && !string.IsNullOrEmpty(textFieldRef.Value))
        {
            var textToCopy = textFieldRef.Value;
            await CopyToClipboardAsync(textToCopy);
            Snackbar.Add("Link copiado para sua área de transferência!", Severity.Success);
        }
    }

    protected async Task CopyToClipboardAsync(string text)
    {
        await JSRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
    }

    protected async Task DownloadFile()
    {
        var fileUrl = $"{NavigationManager.BaseUri}{pdfFilePath}";
        await JSRuntime.InvokeVoidAsync("open", fileUrl, "_blank");
    }
}