﻿using FluentResults;
using FluentResults.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Priemka.API.Authorization;
using Priemka.API.Contracts;
using Priemka.Application.Doctors.AddPatient;
using Priemka.Application.Doctors.Create;
using Priemka.Application.Doctors.Get;
using Priemka.Application.Doctors.GiveAppointment;
using Priemka.Application.Doctors.Update;
using Priemka.Domain.Common;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Priemka.API.Controllers
{
    public class DoctorsController : ApplicationController
    {
        private readonly ISender _sender;

        public DoctorsController(ISender sender)
        {
            _sender = sender;
        }


        [HttpGet("getAll")]
        //[Permission(permissions: Permissions.Doctors.Read)]
        public async Task<IActionResult> GetDoctors(CancellationToken cancellationToken)
        {
            var result = await _sender.Send(new GetDoctorQuery(), cancellationToken);
            if (result.IsFailed)
                return BadRequest(string.Join(", ", result.Reasons.Select(r => r.Message)));

            var doctorsDto = result.Value.Select(d => new DoctorDto
            {
                Id = d.Id,
                Name = d.FullName.FirstName,
                LastName = d.FullName.LastName,
                Phone = d.Phone.Number,
                OnVacation = d.OnVacation,
                Patients = d.Patients.Select(p => new PatientDto
                {
                    Id = p.Id,
                    FirsName = p.FullName.FirstName,
                    LastName = p.FullName.LastName,
                    Phone = p.Phone.Number,
                    Age = p.Age,
                    Appointments = d.Appointments.Select(a => new AppointmentDto
                    {
                        Summary = a.Summary,
                        Description = a.Description,
                        AppoinmentDate = a.AppointmentDate
                    })
                })
            }); 

            return Ok(doctorsDto);
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

        [HttpPost("addPatient")]
        public async Task<IActionResult> AddPatient(AddPatientCommand command, CancellationToken ct)
        {
            return DefaultActionResult(await _sender.Send(command, ct));
        }

        [HttpPost("giveAppointment")]
        public async Task<IActionResult> GiveAppointment(GiveAppointmentCommand command, CancellationToken ct)
        {
            return DefaultActionResult(await _sender.Send(command, ct));
        }
    }
}
