using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.GetResearcher.Contracts;

namespace UxTracker.Core.Contexts.Account.UseCases.GetResearcher;

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
                return new Response("Perfil não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar o usuário", 500);
        }

        #endregion
        
        #region 03. Retornar os dados

        try
        {
            return new Response(string.Empty, new ResponseData(user.Id.ToString(), user.Name, user.Email, user.Email.ToSha256Hash()));
        }
        catch
        {
            return new Response("Não foi possível retornar os dados do perfil", 500);
        }

        #endregion
    }
}