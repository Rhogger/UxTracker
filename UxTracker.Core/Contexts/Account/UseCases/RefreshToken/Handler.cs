using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.RefreshToken.Contracts;

namespace UxTracker.Core.Contexts.Account.UseCases.RefreshToken;

public class Handler: IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;
    private readonly IService _service;

    public Handler(IRepository repository,IService service)
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

        #region 02. Recuperar usuário do banco

        Researcher? user;

        try
        {
            user = await _repository.GetUserByIdAsync(request.Id, cancellationToken);

            if (user is null)
                return new Response("Usuário não encontrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar o usuário", 500);
        }

        #endregion

        #region 03. Gerar os tokens JWT

        string accessToken;
        string refreshToken;

        try
        {
            accessToken = _service.GenerateAccessToken(user, cancellationToken);
            refreshToken = _service.GenerateRefreshToken(user, cancellationToken);
        }
        catch
        {
            return new Response("Não foi possível gerar os tokens ",500);
        }

        #endregion
        
        #region 04. Retornar os dados

        try
        {
            return new Response(string.Empty, new ResponseData(accessToken, refreshToken));
        }
        catch
        {
            return new Response("Não foi possível retornar os dados dos tokens", 500);
        }

        #endregion
    }
}