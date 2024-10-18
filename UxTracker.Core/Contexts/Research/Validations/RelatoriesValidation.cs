using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Research.Validations;

public static class RelatoriesValidation
{
    public static Contract<Notification> EnsureRelatories(List<string> ids)
        => new Contract<Notification>()
            .Requires()
            .IsGreaterOrEqualsThan(ids.Count, 1, "Relatories", "Deve-se selecionar pelo menos um relatório");
}