namespace UxTracker.Core.Contexts.Research.DTOs;

public class ReviewsDto
{
    public Guid UserId { get; set; }
    public string Email { get; set; } = string.Empty;
    public int Index { get; set; } 
    public decimal Rate { get; set; }
    public string Comment { get; set; } = string.Empty;
}