using MediatR;
using Microsoft.AspNetCore.Mvc;
using Priemka.API.Contracts;
using Priemka.Application.Doctors.Get;
using Priemka.Application.Patients;

namespace Priemka.API.Controllers
{
    public class PatientsController : ApplicationController
    {
        private readonly ISender _sender;

        public PatientsController(ISender sender)
        {
            _sender = sender;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {

            var result = await _sender.Send(new GetPatientsQuery(), cancellationToken);
            if (result.IsFailed)
                return BadRequest(string.Join(", ", result.Reasons.Select(r => r.Message)));

            var patients = result.Value.Select(p => new PatientDto
            {
                Id = p.Id,
                FirsName = p.FullName.FirstName,
                LastName = p.FullName.LastName,
                Phone = p.Phone.Number,
                Age = p.Age
            });
            return Ok(patients);
        }
    }
}
