using System.Security.Claims;
using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.GetUser.Contracts;
using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Contexts.Account.UseCases.GetUser;

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

        User? user;

        #region 02. Recuperar usuário do banco

        try
        {
            user = await _repository.GetUserByIdAsync(request.Id, cancellationToken);

            if (user is null)
                return new Response("Perfil não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível recuperar seu perfil", 500);
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