using UxTracker.Core.Contexts.Review.UseCases.AcceptTerm.Contracts;
using UxTracker.Core.Contexts.Review.Entities;
using UxTracker.Infra.Data;

namespace UxTracker.Infra.Contexts.Review.UseCases.AcceptTerm;

public class Repository(AppDbContext context) : IRepository
{
    public async Task AcceptTermAsync(UserAcceptedTcle tcle, CancellationToken cancellationToken)
    {
        await context.AcceptedTerms.AddAsync(tcle, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }
}