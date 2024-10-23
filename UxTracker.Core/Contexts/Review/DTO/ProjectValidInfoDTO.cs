using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Review.DTO;

public class ProjectValidInfoDTO
{
    public Status Status { get; set; }
    public PeriodType PeriodType { get; set; }
    public int SurveyCollections { get; set; }
}