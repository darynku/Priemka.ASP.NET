using FluentResults;
using MediatR;

namespace Priemka.Application.Users.Login
{
    public record LoginCommand(string Email, string Password) : IRequest<Result<LoginRespones>>;

}