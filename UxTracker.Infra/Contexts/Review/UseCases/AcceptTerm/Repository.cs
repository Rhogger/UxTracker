using UxTracker.Core.Contexts.Review.UseCases.AcceptTerm.Contracts;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Review.UseCases.AcceptTerm;

public class Repository : IRepository
{
    private readonly AppDbContext _context;

    public Repository(AppDbContext context) => _context = context;

    public async Task AcceptTermAsync(UserAcceptedTcle tcle, CancellationToken cancellationToken)
    {
        await _context.AcceptedTerms.AddAsync(tcle, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
}