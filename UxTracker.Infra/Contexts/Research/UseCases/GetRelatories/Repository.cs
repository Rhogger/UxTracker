using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.GetRelatories.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.GetRelatories;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    } 

    public async Task<List<GetRelatoriesDTO>?> GetRelatoriesAsync(CancellationToken cancellationToken) =>
        await _context
            .Relatories
            .AsNoTracking()
            .Select(x => new GetRelatoriesDTO
            {
                Id = x.Id,
                Title = x.Title,
            })
            .ToListAsync(cancellationToken: cancellationToken);
}