using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.ResendResetCode.Contracts;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendResetCode;

public class Handler(IRepository repository, IService service) : IRequestHandler<Request, Response>
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
            user = await repository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                return new Response("Usuário não cadastrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar o usuário", 500);
        }

        #endregion
        
        #region 03. Checar se a conta está verificada

        try
        {
            if (!user.Email.Verification.IsActive)
                return new Response("Esta conta não está verificada", 400);
        }
        catch
        {
            return new Response("Não foi possível verificar seu perfil", 500);
        }

        #endregion
        
        #region 04. Gerar novo código

        try
        {
            user.Password?.GenerateResetCode();

            await repository.UpdateResetCodeAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Ocorreu um erro ao atualizar o código", 500);
        }

        #endregion
        
        #region 05. Enviar o novo código por e-mail

        try
        {
            await service.ResendResetCodeAsync(user, cancellationToken);
        }
        catch
        {
            //Nada aqui
        }

        #endregion

        #region 06. Retornar os dados

        return new Response("Código reenviado com sucesso!", 200);

        #endregion
    }
}