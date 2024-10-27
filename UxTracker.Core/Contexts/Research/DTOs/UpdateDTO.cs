using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Research.DTOs;

public class UpdateDTO
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public PeriodType PeriodType { get; set; }
    public int SurveyCollections { get; set; }
    public List<GetRelatoriesDTO> Relatories { get; set; } = new();
}