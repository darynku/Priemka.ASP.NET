using FluentResults;
using MediatR;
using Priemka.Domain.ValueObjects;

namespace Priemka.Application.Doctors.GiveAppointment;

public record GiveAppointmentCommand(
    Guid DoctorId, 
    Guid PatientId, 
    IEnumerable<Medication> Medications,
    DateTime AppointmentDate,
    string Summary,
    string Description) : IRequest<Result>;
