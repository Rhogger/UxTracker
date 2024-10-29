using MediatR;
using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Research.UseCases.Create;

public class Request : IRequest<Response>
{
    public string UserId { get; set; } = string.Empty;
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int SurveyCollections { get; set; }
    public string ConsentTermHash { get; set; } = string.Empty;
    public PeriodType PeriodType { get; set; }
    public List<string> Relatories { get; set; } = new();
}