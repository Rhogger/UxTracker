using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.UpdateReviewer.Contracts;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdateReviewer;

public class Handler: IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    
    public Handler(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Validar requisição

        Contract<Notification> req;
        
        try
        {
            req = Specification.Ensure(request);
            
            if (!req.IsValid)
                return new Response("Requisição inválida", 400, req.Notifications);
        }
        catch
        {
            return new Response("Não foi possível validar sua requisição", 500);
        }

        #endregion
        
        #region 02. Recuperar usuário do banco

        Reviewer? user;

        try
        {
            user = await _repository.GetUserByIdAsync(request.Id, cancellationToken);

            if (user is null)
                return new Response("Usuário não cadastrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar o usuário", 500);
        }

        #endregion

        #region 03. Atualizar o objeto user

        if(user.IsNewCountry(request.Country))
            user.UpdateCountry(request.Country);
        
        if(user.IsNewState(request.State))
            user.UpdateState(request.State);
        
        if(user.IsNewCity(request.City))
            user.UpdateCity(request.City);
        
        if(user.IsNewSex(request.Sex))
            user.UpdateSex(request.Sex);

        #endregion
        
        #region 04. Alterar informações do usuário

        try
        {
            await _repository.UpdateAccountAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao atualizar os dados da conta", 500);
        }
        #endregion

        #region 05. Retorna os dados

        return new Response("Dados da conta atualizado com sucesso!", 200);

        #endregion
    }
}