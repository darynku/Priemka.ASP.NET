using FluentResults;
using MediatR;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;
using Priemka.Domain.ValueObjects;

namespace Priemka.Application.Doctors.GiveAppointment;

public class GiveAppointmentCommandHandler : IRequestHandler<GiveAppointmentCommand, Result>
{
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IPatientRepository _petientRepository;

    public GiveAppointmentCommandHandler(IDoctorsRepository doctorsRepository, IPatientRepository petientRepository)
    {
        _doctorsRepository = doctorsRepository;
        _petientRepository = petientRepository;
    }

    public async Task<Result> Handle(GiveAppointmentCommand request, CancellationToken cancellationToken)
    {

        var patient = await _petientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if(patient.IsFailed) return patient.ToResult();

        var medication = request.Medications.Select(m => Medication.Create(m.Name, m.Dosage));
        var appointment = Appointment.Create(Guid.NewGuid(), request.AppointmentDate, request.Summary, request.Description, medication.Select(m => m.Value));

        patient.Value.AddAppointment(appointment.Value);

        return Result.Ok();

    }
}
