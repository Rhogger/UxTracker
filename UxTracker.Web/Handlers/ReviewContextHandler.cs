using RestSharp;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Review.Handlers;
using AcceptTerm = UxTracker.Core.Contexts.Review.UseCases.AcceptTerm;
using Rating = UxTracker.Core.Contexts.Review.UseCases.Rating;

namespace UxTracker.Web.Handlers;

public class ReviewContextHandler(
    IAccountContextHandler accountContextHandler,
    IRestClient restClient,
    ICookieHandler cookieHandler)
    : IReviewContextHandler
{
    public async Task<RestResponse<AcceptTerm.Response>?> AcceptTermAsync(AcceptTerm.Request requestModel)
    {
        try
        {
            var request = new RestRequest("/api/v1/review/accept-term/", Method.Post)
                .AddJsonBody(requestModel);

            var token = await cookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await cookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await accountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await cookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    throw new Exception("Necessário logar novamente.");
                }
            }
        
            var response = await restClient.ExecuteAsync<AcceptTerm.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                        return response;
                    else
                        throw new Exception(
                            $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");
                else
                    return response;
            throw new Exception($"{response.StatusCode} - {response.Content}");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
    
    public async Task<RestResponse<Rating.Response>?> RatingAsync(Rating.Request requestModel)
    {
        try
        {
            var request = new RestRequest($"/api/v1/review/{requestModel.ProjectId}", Method.Post)
                .AddJsonBody(requestModel);

            var token = await cookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await cookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await accountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await cookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    throw new Exception("Necessário logar novamente.");
                }
            }
        
            var response = await restClient.ExecuteAsync<Rating.Response>(request);

            if (response.Data is not null)
                if (response.IsSuccessful)
                    if (response.Data.StatusCode == 200)
                        return response;
                    else
                        throw new Exception(
                            $"Status Code {response.Data.StatusCode} - Mensagem: {response.Data.Message}");
                else
                    return response;
            throw new Exception($"{response.StatusCode} - {response.Content}");
        }
        catch (Exception ex)
        {
            throw new Exception($"{ex.Message}");
        }
    }
}