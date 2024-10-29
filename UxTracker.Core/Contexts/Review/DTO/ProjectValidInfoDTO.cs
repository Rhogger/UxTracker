using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Review.DTO;

public class ProjectValidInfoDto
{
    public Status Status { get; init; }
    public PeriodType PeriodType { get; init; }
    public int SurveyCollections { get; init; }
}