using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Research.DTOs;

public class GetDto
{
    public string? Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public Status Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public PeriodType PeriodType { get; set; }
    public int SurveyCollections { get; set; }
    public int LastSurveyCollection { get; init; }
    public int ReviewsCount { get; init; }
    public int ReviewersCount { get; init; }
    public string? ConsentTermName { get; set; } = string.Empty;
    public string ConsentTermUrl { get; set; } = string.Empty;
    public string ResearchUrl { get; set; } = string.Empty;
    public List<GetRelatoriesDto>? Relatories { get; set; } = [];
    public List<ReviewsDto>? Reviews { get; set; } = [];
}