using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.Delete.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.Delete;

public class Repository(AppDbContext context) : IRepository
{
    private IDbContextTransaction? _transaction;

    public async Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken) => await context
        .Projects
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);

    public async Task DeleteProjectAsync(Project project, CancellationToken cancellationToken)
    {
        context
            .Projects
            .Remove(project);

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