using MediatR;

namespace UxTracker.Core.Contexts.Account.UseCases.Create;

public record Request(string Name, string Email, string Password) : IRequest<Response>;
