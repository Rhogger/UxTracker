using RestSharp;
using AuthenticateResearcher = UxTracker.Core.Contexts.Account.UseCases.AuthenticateResearcher;
using AuthenticateReviewer = UxTracker.Core.Contexts.Account.UseCases.AuthenticateReviewer;
using CreateResearch = UxTracker.Core.Contexts.Account.UseCases.CreateResearcher;
using CreateReviewer = UxTracker.Core.Contexts.Account.UseCases.CreateReviewer;
using DeleteResearcher = UxTracker.Core.Contexts.Account.UseCases.DeleteResearcher;
using DeleteReviewer = UxTracker.Core.Contexts.Account.UseCases.DeleteReviewer;
using GetResearcher = UxTracker.Core.Contexts.Account.UseCases.GetResearcher;
using GetReviewer = UxTracker.Core.Contexts.Account.UseCases.GetReviewer;
using PasswordRecovery = UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery;
using PasswordRecoveryVerify = UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;
using RefreshToken = UxTracker.Core.Contexts.Account.UseCases.RefreshToken;
using ResendResetCode = UxTracker.Core.Contexts.Account.UseCases.ResendResetCode;
using ResendVerificationCodeResearcher = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeResearcher;
using ResendVerificationCodeReviewer = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCodeReviewer;
using UpdateResearcher = UxTracker.Core.Contexts.Account.UseCases.UpdateResearcher;
using UpdateReviewer = UxTracker.Core.Contexts.Account.UseCases.UpdateReviewer;
using UpdatePassword = UxTracker.Core.Contexts.Account.UseCases.UpdatePassword;
using VerifyResearcher = UxTracker.Core.Contexts.Account.UseCases.VerifyResearcher;
using VerifyReviewer = UxTracker.Core.Contexts.Account.UseCases.VerifyReviewer;

namespace UxTracker.Core.Contexts.Account.Handlers;

public interface IAccountContextHandler
{
    public Task<RestResponse<AuthenticateResearcher.Response>?> SignInResearcherAsync(AuthenticateResearcher.Request requestModel);
    public Task<RestResponse<AuthenticateReviewer.Response>?> SignInReviewerAsync(AuthenticateReviewer.Request requestModel);
    public Task<RestResponse<CreateResearch.Response>?> SignUpResearchAsync(CreateResearch.Request requestModel);
    public Task<RestResponse<CreateReviewer.Response>?> SignUpReviewerAsync(CreateReviewer.Request requestModel);
    public Task SignOutAsync();
    public Task<RestResponse<PasswordRecovery.Response>?> SendResetCodeAsync(PasswordRecovery.Request requestModel);
    public Task<RestResponse<VerifyResearcher.Response>?> VerifyResearcherAsync(VerifyResearcher.Request requestModel);
    public Task<RestResponse<VerifyReviewer.Response>?> VerifyReviewerAsync(VerifyReviewer.Request requestModel);
    public Task<RestResponse<RefreshToken.Response>?> RefreshTokenAsync();
    public Task<RestResponse<ResendVerificationCodeResearcher.Response>?> ResendVerificationCodeResearcherAsync(
        ResendVerificationCodeResearcher.Request requestModel);
    public Task<RestResponse<ResendVerificationCodeReviewer.Response>?> ResendVerificationCodeReviewerAsync(
        ResendVerificationCodeReviewer.Request requestModel);
    public Task<RestResponse<ResendResetCode.Response>?> ResendResetCodeAsync(
        ResendResetCode.Request requestModel);
    public Task<RestResponse<PasswordRecoveryVerify.Response>?> PasswordRecoveryVerifyAsync(
        PasswordRecoveryVerify.Request requestModel);
    public Task<RestResponse<UpdatePassword.Response>?> UpdatePasswordAsync(UpdatePassword.Request requestModel);
    public Task<RestResponse<GetResearcher.Response>?> GetResearcherAsync();
    public Task<RestResponse<GetReviewer.Response>?> GetReviewerAsync();
    public Task<RestResponse<UpdateResearcher.Response>?> UpdateResearcherAsync(UpdateResearcher.Request requestModel);
    public Task<RestResponse<UpdateReviewer.Response>?> UpdateReviewerAsync(UpdateReviewer.Request requestModel);
    public Task<RestResponse<DeleteResearcher.Response>?> DeleteResearcherAsync(DeleteResearcher.Request requestModel);
    public Task<RestResponse<DeleteReviewer.Response>?> DeleteReviewerAsync(DeleteReviewer.Request requestModel);
}