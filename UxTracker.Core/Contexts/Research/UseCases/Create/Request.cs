using MediatR;
using UxTracker.Core.Contexts.ResearchContext.Enums;

namespace UxTracker.Core.Contexts.Research.UseCases.Create;

public class Request : IRequest<Response>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public PeriodType PeriodType { get; set; } = PeriodType.Daily;
    public int Period { get; set; }
}