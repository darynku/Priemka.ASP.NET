using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
        var result = await _sender.Send(command, ct);
        if (result.IsFailed)
            return BadRequest(string.Join(",", result.Reasons.Select(reason => reason.Message)));

        var context = HttpContext;
        context.Response.Cookies.Append("cookie", result.Value.Token);
        return Ok(result.Value);
    }
    [HttpPost("resister")]
    public async Task<IActionResult> Register(RegisterCommand command, CancellationToken ct)
    {
        var result = await _sender.Send(command, ct);
        if (result.IsFailed)
            return BadRequest(string.Join(",", result.Reasons.Select(reason => reason.Message)));
        return Ok();
    }
}
