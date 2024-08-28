using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.Delete;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .IsLowerOrEqualsThan(request.Password.Length, 40, "Password", "A senha deve conter no máximo 40 caracteres")
            .IsGreaterOrEqualsThan(request.Password.Length, 8, "Password", "A senha deve conter pelo menos 8 caracteres")
            .Requires();
}