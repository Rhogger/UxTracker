using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdateReviewer;

public class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.Sex.ToString(), "Sex", "Sexo inválido")
            .IsNotNullOrEmpty(request.Country, "Country", "Pais inválido")
            .IsNotNullOrEmpty(request.State, "State", "Estado inválido")
            .IsNotNullOrEmpty(request.City, "City", "Cidade inválido");
}