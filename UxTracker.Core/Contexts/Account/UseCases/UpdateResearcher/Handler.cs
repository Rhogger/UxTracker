using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.UpdateResearcher.Contracts;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdateResearcher;

public class Handler(IRepository repository) : IRequestHandler<Request, Response>
{
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

        Researcher? user;

        try
        {
            user = await repository.GetUserByIdAsync(request.Id, cancellationToken);

            if (user is null)
                return new Response("Usuário não cadastrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar o usuário", 500);
        }

        #endregion

        #region 03. Validar nova requisição
        
        if (user.Password != null && user.Password.IsValid(request.Password))
            req.AddNotification("Password", "A nova senha é igual a atual");
        
        if (req.Notifications.Count != 0)
            return new Response("Erro ao atualizar dados do perfil", 400, req.Notifications);

        #endregion

        #region 04. Atualizar o objeto user

        if(user.IsNewName(request.Name))
            user.UpdateName(request.Name);
        
        user.UpdatePassword(request.Password);

        #endregion
        
        #region 05. Alterar informações do usuário

        try
        {
            await repository.UpdateAccountAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao atualizar os dados da conta", 500);
        }
        #endregion

        #region 06. Retorna os dados

        return new Response("Dados da conta atualizado com sucesso!", 200);

        #endregion
    }
}