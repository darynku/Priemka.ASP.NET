using Priemka.Domain.Common;
using FluentResults;
using Priemka.Domain.ValueObjects;
namespace Priemka.Domain.Entities;

public class Appointment : Entity
{
    private Appointment(
        Guid id,
        Guid patientId,
        Guid doctorId,
        DateTime appointmentDate,
        string summary,
        string description) : base(id) 
    {
        PatientId = patientId;
        DoctorId = doctorId;
        AppointmentDate = appointmentDate;
        Summary = summary;
        Description = description;
    }
    private Appointment() { }
    
    public Patient? Patient { get; private set; }
    public Doctor? Doctor { get; private set; }
    public Guid PatientId { get; private set; } = Guid.Empty;
    public Guid DoctorId { get; private set; } = Guid.Empty;

    public IReadOnlyList<Medication> Medications => _medications;
    private readonly List<Medication> _medications = new();
    
    public DateTime AppointmentDate { get; private set; }

    public string Summary { get; private set; } = null!;
    public string Description { get; private set; } = null!;


    public static Result<Appointment> Create(Guid id, Guid patientId, Guid doctorId, DateTime appointmentDate, string summary, string description)
    {
        if (patientId == Guid.Empty)
            return Result.Fail("ID пациента не может быть пустым");
        if (doctorId == Guid.Empty)
            return Result.Fail("ID врача не может быть пустым");
        if (appointmentDate == default)
            return Result.Fail("Дата приема не может быть пустой");
        if (string.IsNullOrWhiteSpace(summary))
            return Result.Fail("Сводка не может быть пустой");
        if (string.IsNullOrWhiteSpace(description))
            return Result.Fail("Описание не может быть пустым");

        var appointment = new Appointment(id, patientId, doctorId, appointmentDate, summary, description);
        return Result.Ok(appointment);
    }

    public Result AddMedication(Medication medication)
    {
        if (medication is null)
            return Result.Fail("Лекарство не может быть пустым");

        _medications.Add(medication);
        return Result.Ok();
    }
}
