using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.Verify;

public record Request(string Email, string VerificationCode) : IRequest<Response>;