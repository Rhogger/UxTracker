using System.Text.Json.Serialization;
using MediatR;

namespace UxTracker.Core.Contexts.Research.UseCases.GetForReview;

public class Request : IRequest<Response>
{
    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
    [JsonIgnore]
    public string ProjectId { get; set; } = string.Empty;
}