using MediatR;
using UxTracker.Core.Contexts.Account.Entities;
using UxTracker.Core.Contexts.Account.UseCases.Create.Contracts;
using UxTracker.Core.Contexts.Account.ValueObjects;

namespace UxTracker.Core.Contexts.Account.UseCases.Create;

public class Handler : IRequestHandler<Request, Response>
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
        #region 01. Validar Requisição
        
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

        #region 02. Gerar os Objetos
        
        Email email;
        Password password;
        User user;
        Role role;

        try
        {
            email = new Email(request.Email);
            password = new Password(request.Password);
            user = new User(request.Name, email, password);

            role = await _repository.GetRoleByNameAsync("Researcher", cancellationToken);

            if (role is null)
                return new Response("Role não encontrada", 404);
            
            _repository.AttachRole(role);
            
            user.Roles.Add(role);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        
        #endregion

        #region 03. Verificar se o usuário existe no banco
        
        try
        {
            var exists = await _repository.AnyAsync(request.Email, cancellationToken);

            if (exists)
                return new Response("Este E-mail já está cadastrado", 400);
        }
        catch
        {
            return new Response("Falha ao verificar E-mail cadastrado", 500);
        }
        
        #endregion

        #region 04. Persistir os dados
        
        try
        {
            await _repository.SaveAsync(user, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao persistir os dados", 500);
        }
        
        #endregion

        #region 05. Envia E-mail de ativação
        
        try
        {
            await _service.SendVerificationEmailAsync(user, cancellationToken);
        }
        catch
        {
            // Nada aqui
        }
        
        #endregion

        #region 06. Retornar os dados
        
        return new Response("Conta criada com sucesso!", 201);

        #endregion
    }
}