using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Research.Validations;

public static class TitleValidation
{
    public static Contract<Notification> EnsureTitle(string title)
        => new Contract<Notification>()
            .Requires()
            .IsLowerOrEqualsThan(title.Length, 80, "Title", "O título deve conter no máximo 80 caracteres")
            .IsGreaterOrEqualsThan(title.Length, 4, "Title", "O título deve conter pelo menos 4 caracteres");
}