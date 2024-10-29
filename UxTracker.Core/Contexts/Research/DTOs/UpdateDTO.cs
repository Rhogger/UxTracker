using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Research.DTOs;

public class UpdateDto
{
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.NotStarted;
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public PeriodType PeriodType { get; set; }
    public int SurveyCollections { get; set; }
    public List<GetRelatoriesDto>? Relatories { get; set; } = new();
}