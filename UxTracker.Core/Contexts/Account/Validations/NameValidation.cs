using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.Validations;

public static class NameValidation
{
    public static Contract<Notification> EnsureName(string name) 
        => new Contract<Notification>()
            .Requires()
            .IsLowerOrEqualsThan(name.Length, 80, "Name", "O nome deve conter no máximo 80 caracteres")
            .IsGreaterOrEqualsThan(name.Length, 3, "Name", "O nome deve conter pelo menos 3 caracteres");
}