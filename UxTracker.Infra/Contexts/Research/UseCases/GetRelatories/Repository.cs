using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.DTOs;
using UxTracker.Core.Contexts.Research.UseCases.GetRelatories.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.GetRelatories;

public class Repository(AppDbContext context) : IRepository
{
    public async Task<List<GetRelatoriesDto>?> GetRelatoriesAsync(CancellationToken cancellationToken) =>
        await context
            .Relatories
            .AsNoTracking()
            .Select(x => new GetRelatoriesDto
            {
                Id = x.Id,
                Title = x.Title,
            })
            .ToListAsync(cancellationToken: cancellationToken);
}