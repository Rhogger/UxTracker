using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Research.DTOs;

public class GetAllDto
{
    public Guid Id { get; init; }
    public string? Title { get; init; } = string.Empty;
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
    public Status Status { get; init; }
    public int ReviewsCount { get; init; }
    public int ReviewersCount { get; init; }
}