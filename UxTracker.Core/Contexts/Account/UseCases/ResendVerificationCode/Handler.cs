using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCode.Contracts;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCode;

public class Handler: IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository, IService service)
    {
        _repository = repository;
        _service = service;
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
        
        #region 04. Gerar novo código

        try
        {
            user.Email.ResendVerification();

            await _repository.UpdateVerificationCodeAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Ocorreu um erro ao atualizar o código", 500);
        }

        #endregion
        
        #region 05. Enviar o novo código por e-mail

        try
        {
            await _service.ResendVerificationCodeAsync(user, cancellationToken);
        }
        catch
        {
            //Nada aqui
        }

        #endregion

        return new Response("Código reenviado com sucesso", new ResponseData(user.Password.ResetCode));
    }
}