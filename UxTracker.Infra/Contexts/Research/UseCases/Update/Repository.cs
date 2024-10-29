using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.Update.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.Update;

public class Repository(AppDbContext context) : IRepository
{
    private IDbContextTransaction? _transaction;


    public async Task<List<Relatory>?> GetRelatoriesByIdAsync(List<string> relatories, CancellationToken cancellationToken) => 
        await context
            .Relatories
            .Where(x => relatories
                .Contains(x.Id.ToString()))
            .ToListAsync(cancellationToken);

    public async Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken) => await context
        .Projects
        .Include(x => x.Relatories)
        .Include(x => x.Reviews)
        .FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);

    public void AttachProject(Project project) => context.Projects.Attach(project);
    public void AttachRelatories(List<Relatory> relatories) => context.Relatories.AttachRange(relatories);

    public async Task UpdateProjectAsync(Project project, CancellationToken cancellationToken)
    {
        context
            .Projects
            .Update(project);

        await context.SaveChangesAsync(cancellationToken);
    }
    
    public async Task CreateTransactionAsync(CancellationToken cancellationToken) =>
        _transaction = await context.Database.BeginTransactionAsync(cancellationToken);

    public async Task RollbackAsync(CancellationToken cancellationToken)
    {
        if(_transaction != null)
        {
            await _transaction.RollbackAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }    
    }
    
    public async Task CommitAsync(CancellationToken cancellationToken)
    {
        if(_transaction != null)
        {
            await _transaction.CommitAsync(cancellationToken);
            await _transaction.DisposeAsync();
            _transaction = null;
        }    
    }
}