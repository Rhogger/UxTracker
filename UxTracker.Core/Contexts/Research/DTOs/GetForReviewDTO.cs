using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Core.Contexts.Review.ValueObjects;

namespace UxTracker.Core.Contexts.Research.DTOs;

public class GetForReviewDTO
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public PeriodType PeriodType { get; set; }
    public int SurveyCollections { get; set; }
    public List<UserRates> Reviews { get; set; } = new();
}