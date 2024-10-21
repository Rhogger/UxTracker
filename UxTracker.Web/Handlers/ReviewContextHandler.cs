using RestSharp;
using UxTracker.Core.Contexts.Account.Handlers;
using UxTracker.Core.Contexts.Review.Handlers;
using AcceptTerm = UxTracker.Core.Contexts.Review.UseCases.AcceptTerm;

namespace UxTracker.Web.Handlers;

public class ReviewContextHandler: IReviewContextHandler
{
    private readonly IRestClient RestClient;
    private readonly ICookieHandler CookieHandler;

    public ReviewContextHandler(IRestClient restClient, ICookieHandler cookieHandler)
    {
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
                throw new Exception("Token JWT não encontrado.");
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
}