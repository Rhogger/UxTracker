using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Shared.Entities;

namespace UxTracker.Core.Contexts.Research.DTOs;

public class ReviewsDTO
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public decimal Rate { get; set; }
    public string Comment { get; set; } = string.Empty;
}