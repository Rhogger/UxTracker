using RestSharp;
using AcceptTerm = UxTracker.Core.Contexts.Review.UseCases.AcceptTerm;

namespace UxTracker.Core.Contexts.Review.Handlers;

public interface IReviewContextHandler
{
    public Task<RestResponse<AcceptTerm.Response>?> AcceptTermAsync(AcceptTerm.Request requestModel);
}