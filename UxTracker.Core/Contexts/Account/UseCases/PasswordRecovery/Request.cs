using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.PasswordRecovery;

public record Request(string Email): IRequest<Response>;