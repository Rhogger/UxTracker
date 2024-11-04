using System.Text.Json.Serialization;
using MediatR;

namespace UxTracker.Core.Contexts.Research.UseCases.UpdateNumberCluster;

public class Request : IRequest<Response>
{
    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
    public string ProjectId { get; set; } = string.Empty;
    public int NumberCluster { get; set; }
}