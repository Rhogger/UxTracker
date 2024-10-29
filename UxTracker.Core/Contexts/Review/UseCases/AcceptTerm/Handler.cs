using MediatR;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Core.Contexts.Review.UseCases.AcceptTerm.Contracts;

namespace UxTracker.Core.Contexts.Review.UseCases.AcceptTerm;

public class Handler(IRepository repository) : IRequestHandler<Request, Response>
{
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

        UserAcceptedTcle tcle;

        try
        {
            tcle = new UserAcceptedTcle(Guid.Parse(request.UserId), Guid.Parse(request.ProjectId), DateTime.UtcNow);
        }
        catch (Exception ex)
        {
            return new Response(ex.Message, 400);
        }
        
        #endregion

        #region 03. Aceitar o termo

        try
        {
            await repository.AcceptTermAsync(tcle, cancellationToken);
        }
        catch
        {
            return new Response("Falha ao aceitar o termo", 500);
        }

        #endregion

        #region 04. Retornar os dados
        
        return new Response("Termo aceito", 200);

        #endregion
    }
}