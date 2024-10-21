using MediatR;
using UxTracker.Core.Contexts.Review.DTO;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Core.Contexts.Review.UseCases.Rating.Contracts;
using UxTracker.Core.Contexts.Review.ValueObjects;

namespace UxTracker.Core.Contexts.Review.UseCases.Rating;

public class Handler : IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }

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

        ProjectValidInfoDTO? infos;
        
        try
        {
            infos = await _repository.GetInfosFromProjectAsync(request.ProjectId, cancellationToken);
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
            rates = await _repository.GetReviewsByUserAsync(request.UserId, request.ProjectId, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao buscar as avaliações", 500);
        }

        #endregion
        
        #region 03. Validar avaliação

        try
        {
            if(rates.Count >= infos.SurveyCollections)
                return new Response("Você já finalizou as avaliações.", 400);
            
            if (rates.Count > 0)
                if (!rates.Last().ValidToRate(infos.PeriodType, rates.Last().RatedAt))
                    return new Response("Não é possível avaliar nesse período, aguarde a próxima avaliação.", 400);
        }
        catch
        {
            return new Response("Falha ao validar avaliação", 500);
        }

        #endregion
        
        #region 04. Gerar os Objetos

        Rate rate;

        try
        {
            rate = new Rate(Guid.Parse(request.UserId), Guid.Parse(request.ProjectId), request.Rating, request.Comment);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        
        #endregion
        
        #region 05. Registrar no banco

        try
        {
            await _repository.RatingAsync(rate, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao armazenar a avaliação", 500);
        }

        #endregion

        #region 06. Retornar os dados

        var userRate = new UserRates
        {
            Index = rates!.Count,
            Rate = rate.Rating,
            Comment = rate.Comment,
            RatedAt = rate.RatedAt
        };
        
        return new Response("Avaliação concluída", new ResponseData(userRate));

        #endregion
    }
}