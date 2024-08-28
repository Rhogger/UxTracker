using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdateAccount;

public class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsLowerOrEqualsThan(request.Name.Length, 80, "Name", "O nome deve conter no máximo 80 caracteres")
            .IsGreaterOrEqualsThan(request.Name.Length, 3, "Name", "O nome deve conter pelo menos 3 caracteres")
            .IsLowerOrEqualsThan(request.Password.Length, 40, "Password", "A senha deve conter no máximo 40 caracteres")
            .IsGreaterOrEqualsThan(request.Password.Length, 8, "Password", "A senha deve conter pelo menos 8 caracteres");
}