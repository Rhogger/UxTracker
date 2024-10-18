using MediatR;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.GetRelatories.Contracts;

namespace UxTracker.Core.Contexts.Research.UseCases.GetRelatories;

public class Handler: IRequestHandler<Request, Response>
{
    private readonly IRepository _repository;

    public Handler(IRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
    {
        #region 01. Recuperar os relatórios

        List<GetRelatoriesDTO>? relatories;

        try
        {
            relatories = await _repository.GetRelatoriesAsync(cancellationToken);

            if (relatories is null)
                return new Response("Nenhum relatório cadastrado", 404);
        }
        catch
        {
            return new Response("Não foi possível encontrar algum relatório", 500);
        }

        #endregion
        
        #region 02. Retornar os dados

        return new Response(string.Empty, new ResponseData(relatories));

        #endregion
    }
}