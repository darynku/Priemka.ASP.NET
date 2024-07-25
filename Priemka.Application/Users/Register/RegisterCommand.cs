using FluentResults;
using MediatR;

namespace Priemka.Application.Users.Register
{
    public record RegisterCommand(
        string Email, 
        string Password) : IRequest<Result>;
}
