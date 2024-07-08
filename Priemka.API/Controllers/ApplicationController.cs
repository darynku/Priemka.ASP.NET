using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Priemka.API.Controllers
{
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        protected IActionResult DefaultActionResult(ResultBase result) 
        {
            if (result.IsFailed)
                return BadRequest(string.Join(",", result.Reasons.Select(reason => reason.Message)));
            return Ok(result);
        }
    }
}
