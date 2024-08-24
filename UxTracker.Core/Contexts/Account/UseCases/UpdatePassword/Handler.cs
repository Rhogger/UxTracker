using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.UpdatePassword.Contracts;

namespace UxTracker.Core.Contexts.Account.UseCases.UpdatePassword;

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
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                return new Response("Perfil não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível recuperar seu perfil", 500);
        }

        #endregion

        #region 03. Alterar a senha

        try
        {
            await _repository.UpdatePasswordAsync(user, request.Password, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao atualizar a senha", 500);
        }
        #endregion

        return new Response("Senha atualizada com sucesso!", 200);
    }
}