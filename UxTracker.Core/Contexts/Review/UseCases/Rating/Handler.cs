using MediatR;
using UxTracker.Core.Contexts.Research.Enums;
using UxTracker.Core.Contexts.Review.DTO;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Core.Contexts.Review.UseCases.Rating.Contracts;
using UxTracker.Core.Contexts.Review.ValueObjects;

namespace UxTracker.Core.Contexts.Review.UseCases.Rating;

public class Handler(IRepository repository) : IRequestHandler<Request, Response>
{
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Validar Requisição
        
        try
        {
            var req = Specification.Ensure(request);

            if (!req.IsValid)
                return new Response("Requisição inválida", 400, req.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }
        
        #endregion

        #region 02. Recuperar informações do projeto

        ProjectValidInfoDto? infos;
        
        try
        {
            infos = await repository.GetInfosFromProjectAsync(request.ProjectId, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao buscar dados do projeto", 500);
        }

        #endregion
        
        #region 03. Recuperar avaliações do usuário

        List<Rate>? rates;
        
        try
        {
            rates = await repository.GetReviewsByUserAsync(request.UserId, request.ProjectId, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao buscar as avaliações", 500);
        }

        #endregion
        
        #region 03. Validar avaliação

        try
        {
            // if(infos != null && infos.Status.Equals(Status.NotStarted))
            //     return new Response("Não é possível avaliar se a pesquisa não iniciou.", 400);        
            //
            // if(infos != null && rates != null && rates.Count >= infos.SurveyCollections)
            //     return new Response("Você já finalizou as avaliações.", 400);
            //
            // if (rates is { Count: > 0 })
            //     if (!Rate.ValidToRate(infos?.PeriodType, rates.Last().RatedAt))
            //         return new Response("Não é possível avaliar nesse período, aguarde a próxima avaliação.", 400);
        }
        catch
        {
            return new Response("Falha ao validar avaliação", 500);
        }

        #endregion
        
        #region 04. Gerar os Objetos

        Rate rate;
        
        var lastRate = rates is not null && rates.Count > 0 
            ? rates.MaxBy(r => r.RatedAt) 
            : null;
        
        var index = lastRate is not null 
            ? lastRate.Index + 1
            : 0;

        try
        {
            rate = new Rate(Guid.Parse(request.UserId), Guid.Parse(request.ProjectId), index, request.Rating, request.Comment);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        
        #endregion
        
        #region 05. Registrar no banco

        try
        {
            await repository.RatingAsync(rate, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao armazenar a avaliação", 500);
        }

        #endregion

        #region 06. Retornar os dados

        if (rates == null) return new Response("Erro ao avaliar", 400);
        
        var userRate = new UserRates
        {
            Index = rates.Count,
            Rate = rate.Rating,
            Comment = rate.Comment,
            RatedAt = rate.RatedAt
        };
        
        return new Response("Avaliação concluída", new ResponseData(userRate));

        #endregion
    }
}