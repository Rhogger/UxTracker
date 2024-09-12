using System.Text.Json.Serialization;
using MediatR;

namespace UxTracker.Core.Contexts.Research.UseCases.GetAll;

public class Request : IRequest<Response>
{
    [JsonIgnore]
    public string UserId { get; set; } = string.Empty;
}