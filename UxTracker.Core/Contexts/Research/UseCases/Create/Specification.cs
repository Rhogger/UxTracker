using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Research.UseCases.Create;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsLowerOrEqualsThan(request.Title.Length, 80, "Title", "O título deve conter no máximo 80 caracteres")
            .IsGreaterOrEqualsThan(request.Title.Length, 4, "Title", "O título deve conter pelo menos 4 caracteres")
            .IsLowerOrEqualsThan(request.StartDate.AddDays(1), request.EndDate, "StartDate",
                "A data inicial deve anteceder 24 horas a data final")
            .IsNotNull(request.Relatories, "Relatories", "A lista de relatórios está vazia")
            .IsNotEmpty(request.Relatories, "Relatories", "Selecione pelo menos um relatório");
}