using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.Get.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.Get;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context)
    {
        _context = context;
    } 

    public async Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken) => await _context
        .Projects
        .AsNoTracking()
        .Include(x => x.Relatories)
        .FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);
}