using MediatR;
using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Review.UseCases.Rating;

public class Request : IRequest<Response>
{
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public int Rating { get; set; }
    public string Comment { get; set; } = string.Empty;
    public PeriodType PeriodType { get; set; }
}