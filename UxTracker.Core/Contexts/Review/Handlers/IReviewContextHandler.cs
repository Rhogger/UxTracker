using RestSharp;
using AcceptTerm = UxTracker.Core.Contexts.Review.UseCases.AcceptTerm;
using Rating = UxTracker.Core.Contexts.Review.UseCases.Rating;

namespace UxTracker.Core.Contexts.Review.Handlers;

public interface IReviewContextHandler
{
    public Task<RestResponse<AcceptTerm.Response>?> AcceptTermAsync(AcceptTerm.Request requestModel);
    public Task<RestResponse<Rating.Response>?> RatingAsync(Rating.Request requestModel);
}