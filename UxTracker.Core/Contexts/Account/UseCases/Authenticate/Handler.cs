using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.Authenticate.Contracts;
using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Contexts.Account.UseCases.Authenticate;

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
        
        #region 02. Recuperar usuário do banco

        User? user;
        
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
                return new Response("Conta inativa", 400);
        }
        catch
        {
            return new Response("Não foi possível verificar seu perfil", 500);
        }

        #endregion
        
        #region 04. Checar validade da senha

        if (!user.Password.IsValid(request.Password))
            return new Response("Usuário ou senha inválida", 400);

        #endregion

        #region 05. Gerar o token JWT

        string token;
        
        try
        {
            token = _service.GenerateJwtToken(user, cancellationToken);
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
                Token = token
            };

            return new Response(string.Empty, data);

        #endregion
    }
}