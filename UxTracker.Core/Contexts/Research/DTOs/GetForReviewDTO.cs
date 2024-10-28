using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Review.ValueObjects;

namespace UxTracker.Core.Contexts.Research.DTOs;

public class GetForReviewDto
{
    public Guid Id { get; init; }
    public string? Title { get; init; } = string.Empty;
    public string? Description { get; init; } = string.Empty;
    public Status Status { get; init; }
    public PeriodType PeriodType { get; init; }
    public int SurveyCollections { get; init; }
    public List<UserRates> Reviews { get; init; } = [];
}