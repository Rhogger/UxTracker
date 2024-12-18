using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.DeleteResearcher.Contracts;

namespace UxTracker.Core.Contexts.Account.UseCases.DeleteResearcher;

public class Handler(IRepository repository) : IRequestHandler<Request, Response>
{
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Validar requisição

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

        #region 02. Recuperar usuário do banco

        Researcher? user;

        try
        {
            user = await repository.GetUserByIdAsync(request.Id, cancellationToken);

            if (user is null)
                return new Response("Usuário não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar o usuário", 500);
        }

        #endregion

        #region 03. Checar validade da senha

        if (user.Password != null && !user.Password.IsValid(request.Password))
            return new Response("Senha inválida", 400);

        #endregion
        
        #region 04. Deletar usuário

        try
        {
            user.UpdateStatusAccount();
            await repository.DeleteUserAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível excluir os dados da conta deste usuário", 500);
        }

        #endregion

        #region 05. Retornar os dados

        return new Response("Conta deletada com sucesso", 200);

        #endregion
    }
}