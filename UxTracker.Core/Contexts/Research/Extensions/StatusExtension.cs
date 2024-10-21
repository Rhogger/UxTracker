using UxTracker.Core.Contexts.Research.Enums;

namespace UxTracker.Core.Contexts.Research.Extensions;

public static class StatusExtension
{
    public static string ToHumanize(this Status status) =>
        status switch
        {
            Status.NotStarted => "Não iniciado",
            Status.InProgress => "Em progresso",
            Status.Finished => "Finalizado",
            _ => throw new ArgumentOutOfRangeException(nameof(status), status, null)
        };
}