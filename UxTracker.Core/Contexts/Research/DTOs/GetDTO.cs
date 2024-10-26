using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Research.DTOs;

public class GetDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public PeriodType PeriodType { get; set; }
    public int SurveyCollections { get; set; }
    public int LastSurveyCollection { get; set; }
    public int ReviewsCount { get; set; }
    public int ReviewersCount { get; set; }
    public string ConsentTermName { get; set; } = string.Empty;
    public string ConsentTermUrl { get; set; } = string.Empty;
    public string ResearchUrl { get; set; } = string.Empty;
    public List<GetRelatoriesDTO> Relatories { get; set; } = new();
    public List<ReviewsDTO> Reviews { get; set; } = new();
}