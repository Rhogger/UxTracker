using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.Verify.Contracts;

namespace UxTracker.Core.Contexts.Account.UseCases.Verify;

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
        
        #region 03. Checar se a conta está verificada

        try
        {
            if (user.Email.Verification.IsActive)
                return new Response("Esta conta já está ativa", 400);
        }
        catch
        {
            return new Response("Não foi possível verificar seu perfil", 500);
        }

        #endregion
        
        #region 04. Checar validade do código

        if (!user.Email.Verification.IsValid(request.VerificationCode))
            return new Response("Código está incorreto", 400);
        
        if (user.Email.Verification.ExpireAt < DateTime.UtcNow)
            return new Response("Código está expirado", 400);

        #endregion
        
        #region 05. Verificar a conta

        try
        {
            await _repository.ValidateVerificationCodeAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível validar a conta deste usuário", 500);
        }
        #endregion

        return new Response("Conta verificada com sucesso!", 200);
    }
}