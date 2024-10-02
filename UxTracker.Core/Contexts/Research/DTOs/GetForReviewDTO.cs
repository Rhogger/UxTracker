using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Research.DTOs;

public class GetForReviewDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public PeriodType PeriodType { get; set; }
    public int SurveyCollections { get; set; }
}