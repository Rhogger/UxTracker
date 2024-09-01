using RestSharp;
using Authenticate = UxTracker.Core.Contexts.Account.UseCases.Authenticate;
using Create = UxTracker.Core.Contexts.Account.UseCases.Create;
using Delete = UxTracker.Core.Contexts.Account.UseCases.Delete;
using GetUser = UxTracker.Core.Contexts.Account.UseCases.GetUser;
using PasswordRecovery = UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery;
using PasswordRecoveryVerify = UxTracker.Core.Contexts.Account.UseCases.PasswordRecoveryVerify;
using RefreshToken = UxTracker.Core.Contexts.Account.UseCases.RefreshToken;
using ResendResetCode = UxTracker.Core.Contexts.Account.UseCases.ResendResetCode;
using ResendVerificationCode = UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCode;
using UpdateAccount = UxTracker.Core.Contexts.Account.UseCases.UpdateAccount;
using UpdatePassword = UxTracker.Core.Contexts.Account.UseCases.UpdatePassword;
using Verify = UxTracker.Core.Contexts.Account.UseCases.Verify;

namespace UxTracker.Core.Contexts.Account.Handlers;

public interface IAccountContextHandler
{
    public Task<RestResponse<Authenticate.Response>?> SignInAsync(Authenticate.Request requestModel);
    public Task<RestResponse<Create.Response>?> SignUpAsync(Create.Request requestModel);
    public Task SignOutAsync();
    public Task<RestResponse<PasswordRecovery.Response>?> SendResetCodeAsync(PasswordRecovery.Request requestModel);
    public Task<RestResponse<Verify.Response>?> VerifyAsync(Verify.Request requestModel);
    public Task<RestResponse<RefreshToken.Response>?> RefreshTokenAsync();
    public Task<RestResponse<ResendVerificationCode.Response>?> ResendVerificationCodeAsync(
        ResendVerificationCode.Request requestModel);
    public Task<RestResponse<ResendResetCode.Response>?> ResendResetCodeAsync(
        ResendResetCode.Request requestModel);
    public Task<RestResponse<PasswordRecoveryVerify.Response>?> PasswordRecoveryVerifyAsync(
        PasswordRecoveryVerify.Request requestModel);
    public Task<RestResponse<UpdatePassword.Response>?> UpdatePasswordAsync(UpdatePassword.Request requestModel);
    public Task<RestResponse<GetUser.Response>?> GetUserAsync();
    public Task<RestResponse<UpdateAccount.Response>?> UpdateAccountAsync(UpdateAccount.Request requestModel);
    public Task<RestResponse<Delete.Response>?> DeleteAccountAsync(Delete.Request requestModel);
}