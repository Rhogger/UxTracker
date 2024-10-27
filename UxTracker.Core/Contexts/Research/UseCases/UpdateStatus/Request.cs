using System.Text.Json.Serialization;
using MediatR;

namespace UxTracker.Core.Contexts.Research.UseCases.UpdateStatus;

public class Request : IRequest<Response>
{
    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public DateTime? StartDate { get; set; } = DateTime.UtcNow;
    public DateTime? EndDate { get; set; }
}