using System.Text.Json.Serialization;
using MediatR;

namespace UxTracker.Core.Contexts.Research.UseCases.GetForReview;

public class Request : IRequest<Response>
{
    [JsonIgnore]
    public string UserId { get; init; } = string.Empty;
    [JsonIgnore]
    public string ProjectId { get; init; } = string.Empty;
}