using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using UxTracker.Core.Contexts.Research.Entities;
using UxTracker.Core.Contexts.Research.UseCases.Delete.Contracts;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Research.UseCases.Delete;

public class Repository : IRepository
{
    private readonly AppDbContext _context;
    private IDbContextTransaction? _transaction;

    public Repository(AppDbContext context)
    {
        _context = context;
    } 

    public async Task<Project?> GetProjectByIdAsync(string id, CancellationToken cancellationToken) => await _context
        .Projects
        .AsNoTracking()
        .FirstOrDefaultAsync(x => x.Id.ToString() == id, cancellationToken);

    public async Task DeleteProjectAsync(Project project, CancellationToken cancellationToken)
    {
        _context
            .Projects
            .Remove(project);

        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task CreateTransactionAsync(CancellationToken cancellationToken) =>
        _transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

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