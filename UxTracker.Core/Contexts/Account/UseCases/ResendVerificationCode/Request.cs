using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.ResendVerificationCode;

public record Request(string Email): IRequest<Response>;