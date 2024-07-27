using Microsoft.AspNetCore.Components;
using RestSharp;
using UxTracker.Core;

namespace UxTracker.Researchers.Web.Pages.Contexts.Account.UseCases.Verify;

public partial class AccountVerification: ComponentBase
{
    protected Core.Contexts.Account.UseCases.Verify.Request Req = new();

    protected async Task VerifyAsync()
    {
        var client = new RestClient();
        var request = new RestRequest("/api/v1/verify")
            .AddJsonBody(Req);
        await client.PatchAsync<Core.Contexts.Account.UseCases.Verify.Request>(request);
    }

    protected async Task ResendVerificationCodeAsync()
    {
        Core.Contexts.Account.UseCases.ResendVerificationCode.Request req = new(Req.Email);
        
        var client = new RestClient();
        var request = new RestRequest("/api/v1/resend-verification-code")
            .AddJsonBody(req);
        await client.PatchAsync<Core.Contexts.Account.UseCases.ResendVerificationCode.Request>(request);
    }
}