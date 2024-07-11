using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Priemka.Application.Users.Login;
using Priemka.Application.Users.Register;

namespace Priemka.API.Controllers;

public class UsersController : ApplicationController
{
    private readonly ISender _sender;

    public UsersController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginCommand command, CancellationToken ct)
    {
        return DefaultActionResult(await _sender.Send(command, ct));
    }
    [HttpPost("resister")]
    public async Task<IActionResult> Register(RegisterCommand command, CancellationToken ct)
    {
        return DefaultActionResult(await _sender.Send(command, ct));
    }
}
