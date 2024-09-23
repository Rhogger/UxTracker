using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Account.UseCases.CreateReviewer;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsEmail(request.Email, "Email", "E-mail inválido")
            .IsGreaterOrEqualsThan(request.BirthDate, DateTime.UtcNow.AddYears(-12), "BirthDate", "Deve se ter pelo menos 12 anos")
            .IsNotNullOrEmpty(request.Sex.ToString(), "Sex", "Sexo inválido")
            .IsNotNullOrEmpty(request.Country, "Country", "Pais inválido")
            .IsNotNullOrEmpty(request.State, "State", "Estado inválido")
            .IsNotNullOrEmpty(request.City, "City", "Cidade inválido");
}