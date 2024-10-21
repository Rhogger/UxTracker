using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Research.Validations;

public static class DescriptionValidation
{
    public static Contract<Notification> EnsureDescription(string description)
        => new Contract<Notification>()
            .Requires()
            .IsLowerOrEqualsThan(description.Length, 2000, "Description", "A descrição deve conter no máximo 2000 caracteres")
            .IsGreaterOrEqualsThan(description.Length, 20, "Description", "A descrição deve conter pelo menos 20 caracteres");
}