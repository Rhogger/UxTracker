using RestSharp;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Review.Handlers;
using AcceptTerm = UxTracker.Core.Contexts.Review.UseCases.AcceptTerm;
using Rating = UxTracker.Core.Contexts.Review.UseCases.Rating;

namespace UxTracker.Web.Handlers;

public class ReviewContextHandler: IReviewContextHandler
{    
    private readonly IAccountContextHandler AccountContextHandler;
    private readonly IRestClient RestClient;
    private readonly ICookieHandler CookieHandler;

    public ReviewContextHandler(IAccountContextHandler accountContextHandler, IRestClient restClient, ICookieHandler cookieHandler)
    {        
        AccountContextHandler = accountContextHandler;
        RestClient = restClient;
        CookieHandler = cookieHandler;
    }
    
    public async Task<RestResponse<AcceptTerm.Response>?> AcceptTermAsync(AcceptTerm.Request requestModel)
    {
        try
        {
            var request = new RestRequest("/api/v1/review/accept-term/", Method.Post)
                .AddJsonBody(requestModel);

            var token = await CookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await CookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await AccountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await CookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    throw new Exception("Necessário logar novamente.");
                }
            }
        
            var response = await RestClient.ExecuteAsync<AcceptTerm.Response>(request);

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

            var token = await CookieHandler.GetAccessToken();
            if (!string.IsNullOrEmpty(token?.Value))
            {
                request.AddHeader("Authorization", $"Bearer {token.Value}");
            }
            else
            {
                var refreshToken = await CookieHandler.GetRefreshToken();
                if (!string.IsNullOrEmpty(refreshToken?.Value))
                {
                    await AccountContextHandler.RefreshTokenAsync();
                    
                    var newToken = await CookieHandler.GetAccessToken();
                    
                    request.AddHeader("Authorization", $"Bearer {newToken?.Value}");
                }
                else
                {
                    throw new Exception("Necessário logar novamente.");
                }
            }
        
            var response = await RestClient.ExecuteAsync<Rating.Response>(request);

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