using MediatR;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Research.UseCases.Create;

public class Request : IRequest<Response>
{
    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int PeriodType { get; set; }
    public int Period { get; set; }
    public PeriodType PeriodType { get; set; }
    // public byte[]? ConsentTerm { get; set; } = null!;
    public List<string> Relatories { get; set; } = new();
}