using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.VerifyReviewer.Contracts;
using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Contexts.Account.UseCases.VerifyReviewer;

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

        Reviewer? user;

        try
        {
            user = await repository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                return new Response("Usuário não cadastrado", 404);
            
            repository.AttachRoles(user.Roles);
        }
        catch
        {
            return new Response("Não foi possível encontrar o usuário", 500);
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
            await repository.ValidateVerificationCodeAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível validar a conta deste usuário", 500);
        }
        #endregion
        
        #region 06. Verificar se usuário informou código de pesquisa

        if(string.IsNullOrEmpty(request.ResearchCode))
            return new Response("Código de pesquisa não informado", 200);

        #endregion
        
        #region 07. Checar validade do código da pesquisa
        
        if (!await repository.AnyProjectAsync(request.ResearchCode, cancellationToken))
            return new Response("Pesquisa não encontrada", 404);

        #endregion
        
        #region 08. Gerar os tokens JWT

        string? accessToken;
        string refreshToken;
        
        try
        {
            accessToken = service.GenerateAccessToken(user, cancellationToken);
            refreshToken = service.GenerateRefreshToken(user, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível gerar as credenciais no servidor", 500);
        }
        
        #endregion
        
        #region 09. Retornar os dados

        var data = new Payload
        {
            Id = user.Id.ToString(),
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
        
        return new Response("Conta verificada com sucesso!", new ResponseData(request.ResearchCode, data));

        #endregion
    }
}