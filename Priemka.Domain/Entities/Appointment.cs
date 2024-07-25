using Priemka.Domain.Common;
using FluentResults;
using Priemka.Domain.ValueObjects;
namespace Priemka.Domain.Entities;

public class Appointment : Entity
{
    private Appointment() { }
    public  Appointment(
        Guid id,
        DateTime appointmentDate,
        string summary,
        string description,
        IEnumerable<Medication> medications) : base(id) 
    {
        AppointmentDate = appointmentDate;
        Summary = summary;
        Description = description;
        _medications = medications.ToList();
    }

    public Patient? Patient { get; private set; }
    public Doctor? Doctor { get; private set; }

    public IReadOnlyList<Medication> Medications => _medications;
    private readonly List<Medication> _medications = new();
    
    public DateTime AppointmentDate { get; private set; }

    public string Summary { get; private set; } = null!;
    public string Description { get; private set; } = null!;


    public static Result<Appointment> Create(Guid id, DateTime appointmentDate, string summary, string description, IEnumerable<Medication> medications)
    {
        if (appointmentDate == default)
            return Result.Fail("Дата приема не может быть пустой");
        if (string.IsNullOrWhiteSpace(summary))
            return Result.Fail("Сводка не может быть пустой");
        if (string.IsNullOrWhiteSpace(description))
            return Result.Fail("Описание не может быть пустым");

        var appointment = new Appointment(id, appointmentDate, summary, description, medications);
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
