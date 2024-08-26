using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify.Contracts;

namespace UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;

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
        
        #region 04. Checar validade do código

        if (!user.Password.IsValidResetCode(request.ResetCode))
            return new Response("Código está incorreto", 400);
        
        if (user.Password.ExpireAt < DateTime.UtcNow)
            return new Response("Código está expirado", 400);

        #endregion
        
        #region 05. Autorizar alteração de senha

        try
        {
            await _repository.ValidateResetCodeAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível validar a autorização", 500);
        }
        #endregion

        #region 06. Retornar os dados

        return new Response("Verificação feita com sucesso!", 200);

        #endregion
    }
}