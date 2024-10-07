using Flunt.Notifications;
using Flunt.Validations;
using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer.Contracts;
using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer;

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

        Contract<Notification> req;
        
        try
        {
            req = Specification.Ensure(request);

            if (request.ResearchCode.Length != 36 && !string.IsNullOrEmpty(request.ResearchCode))
                req.AddNotification("ResearchCode", "Código inválido");

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
            user = await _repository.GetUserByEmailAsync(request.Email, cancellationToken);

            if (user is null)
                return new Response("Usuário não cadastrado", 404, new ResponseData(request.ResearchCode));
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
                return new Response("Conta inativa", 400);
        }
        catch
        {
            return new Response("Não foi possível verificar seu perfil", 500);
        }

        #endregion
        
        #region 04. Checar validade do código da pesquisa
        
        if (!await _repository.AnyProjectAsync(request.ResearchCode, cancellationToken))
            return new Response("Pesquisa não encontrada", 404);

        #endregion

        #region 05. Gerar os tokens JWT

        string accessToken;
        string refreshToken;
        
        try
        {
            accessToken = _service.GenerateAccessToken(user, cancellationToken);
            refreshToken = _service.GenerateRefreshToken(user, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível gerar as credenciais no servidor", 500);
        }
        
        #endregion
        
        #region 06. Retornar os dados

        var data = new Payload
        {
            Id = user.Id.ToString(),
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };

        return new Response(string.Empty, new ResponseData(request.ResearchCode, data));

        #endregion
    }
}