using System.Text.Json.Serialization;
using MediatR;
using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Research.UseCases.Create;

public class Request : IRequest<Response>
{
    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; } = DateTime.UtcNow;
    public int SurveyCollections { get; set; }
    public PeriodType PeriodType { get; set; }
    public List<string> Relatories { get; set; } = new();
}