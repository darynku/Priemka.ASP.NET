using FluentResults;
using Priemka.Domain.Common;

namespace Priemka.Domain.ValueObjects;

public class DoctorTicket : ValueObject
{
    private DoctorTicket() { }
    private DoctorTicket(string summary, string description, DateTime appointmentDate, IEnumerable<Medication> medicications)
    {
        Summary = summary;
        Description = description;
        AppointmentDate = appointmentDate;
        _medications = medicications.ToList();
    }

    public string Summary { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    public DateTime AppointmentDate { get; private set; }
    public IReadOnlyList<Medication> Medications => _medications;
    private readonly List<Medication> _medications = [];

    public static Result<DoctorTicket> Create(string summary, string description, DateTime appointmentDate, IEnumerable<Medication> medicications)
    {
        if (string.IsNullOrEmpty(summary))
            return Result.Fail("пустое описание (короткое)");

        if(string.IsNullOrEmpty(description))
            return Result.Fail("пустое описание");
            
        return new DoctorTicket(summary, description, appointmentDate, medicications);
    }
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Summary;
        yield return Description;
        yield return AppointmentDate;
        yield return Medications;
    }
}
