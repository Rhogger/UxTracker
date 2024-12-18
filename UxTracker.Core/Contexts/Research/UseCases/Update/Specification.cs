using Flunt.Notifications;
using Flunt.Validations;

namespace UxTracker.Core.Contexts.Research.UseCases.Update;

public static class Specification
{
    public static Contract<Notification> Ensure(Request request)
        => new Contract<Notification>()
            .Requires()
            .IsNotNullOrEmpty(request.UserId, "UserId", "Erro ao vincular o usuário")
            .IsLowerOrEqualsThan(request.Title!.Length, 80, "Title", "O título deve conter no máximo 80 caracteres")
            .IsGreaterOrEqualsThan(request.Title.Length, 4, "Title", "O título deve conter pelo menos 4 caracteres")
            .IsLowerOrEqualsThan(request.Description!.Length, 2000, "Description", "A descrição deve conter no máximo 2000 caracteres")
            .IsGreaterOrEqualsThan(request.Description.Length, 20, "Description", "A descrição deve conter pelo menos 20 caracteres")
            .IsGreaterOrEqualsThan(request.SurveyCollections, 1, "SurveyCollections", "Deve-se ter pelo menos 1 coleta")
            .IsNotNull(request.Relatories, "Relatories", "A lista de relatórios está vazia")
            .IsNotEmpty(request.Relatories, "Relatories", "Selecione pelo menos um relatório");
}