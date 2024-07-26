using FluentResults;
using MediatR;
using Priemka.Application.DataAccess;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;
using Priemka.Domain.ValueObjects;

namespace Priemka.Application.Doctors.GiveAppointment;

public class GiveAppointmentCommandHandler : IRequestHandler<GiveAppointmentCommand, Result>
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _petientRepository;
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GiveAppointmentCommandHandler(IPatientRepository petientRepository, IAppointmentRepository appointmentRepository, IUnitOfWork unitOfWork, IDoctorsRepository doctorsRepository)
    {
        _petientRepository = petientRepository;
        _appointmentRepository = appointmentRepository;
        _unitOfWork = unitOfWork;
        _doctorsRepository = doctorsRepository;
    }

    public async Task<Result> Handle(GiveAppointmentCommand request, CancellationToken cancellationToken)
    {

        var patient = await _petientRepository.GetByIdAsync(request.PatientId, cancellationToken);
        if(patient.IsFailed) return patient.ToResult();

        var doctor = await _doctorsRepository.GetById(request.DoctorId, cancellationToken);
        if(doctor.IsFailed) return doctor.ToResult();

        var medication = request.Medications.Select(m => Medication.Create(m.Name, m.Dosage));
        var appointment = Appointment.Create(Guid.NewGuid(), request.AppointmentDate, request.Summary, request.Description, medication.Select(m => m.Value));

        patient.Value.AddAppointment(appointment.Value);
        doctor.Value.AddAppointment(appointment.Value);

        await _appointmentRepository.AddAsync(appointment.Value, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();

    }
}
