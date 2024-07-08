using FluentResults;
using FluentResults.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Priemka.Application.Doctors.Create;
using Priemka.Application.Doctors.Get;
using Priemka.Application.Doctors.Update;

namespace Priemka.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ApplicationController
    {
        private readonly ISender _sender;

        public DoctorsController(ISender sender)
        {
            _sender = sender;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> GetDoctors(GetDoctorQuery query, CancellationToken cancellationToken)
        {
            return DefaultActionResult(await _sender.Send(query, cancellationToken));
        }
        [HttpPost("create")]
        public async Task<IActionResult> CreateDoctor(CreateDoctorCommand command, CancellationToken ct)
        {
           return DefaultActionResult(await _sender.Send(command, ct));
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateDoctor(UpdateDoctorCommand command, CancellationToken ct)
        {
            return DefaultActionResult(await _sender.Send(command, ct));    
        }
    }
}
