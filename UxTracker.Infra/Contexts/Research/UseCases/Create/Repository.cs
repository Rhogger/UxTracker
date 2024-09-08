using Microsoft.EntityFrameworkCore;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.Create.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.Create;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task<List<Relatory>?> GetRelatoriesById(List<string> relatories, CancellationToken cancellationToken) => 
        await _context
            .Relatories
            .AsNoTracking()
            .Where(x => relatories
                .Contains(x.Id.ToString()))
            .ToListAsync(cancellationToken);
    
    public void AttachRelatories(List<Relatory> relatories) => _context.Relatories.AttachRange(relatories);

    public async Task SaveAsync(Project project, CancellationToken cancellationToken)
    {
        await _context.Projects.AddAsync(project, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}