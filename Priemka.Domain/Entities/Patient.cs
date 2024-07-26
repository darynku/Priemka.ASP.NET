using Priemka.Domain.Common;
using Priemka.Domain.ValueObjects;
using FluentResults;
namespace Priemka.Domain.Entities;

public class Patient : Entity
{
    private Patient() { }
    public Patient(
        Guid id,
        FullName fullName, 
        Phone phone,
        Email email,
        Address address,
        int age,
        DateTime birthDay,
        string trouble,
        string description) : base(id)
    {
        FullName = fullName;
        Phone = phone;
        Email = email;
        Address = address;
        BirthDay = birthDay;
        Age = age;
        Trouble = trouble;
        Description = description;
    }
    
    public Guid DoctorId { get; private set; }
    
    public FullName FullName { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Address Address { get; private set; } = null!;

    private readonly List<Appointment> _appointments = [];
    public IReadOnlyList<Appointment> Appointments => _appointments;

    public DateTime BirthDay { get; private set; }
    public int Age { get; private set; }

    public string Trouble { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    
    public static Result<Patient> Create(
        FullName fullName, 
        Phone phone,
        Email email,
        Address address,
        int age,
        DateTime birthDay,
        string trouble, 
        string description)
    {
        if (birthDay == default)
            return Result.Fail("Дата рождения не может быть пустой");
        if (string.IsNullOrWhiteSpace(trouble))
            return Result.Fail("Проблема не может быть пустой");
        if (string.IsNullOrWhiteSpace(description))
            return Result.Fail("Описание не может быть пустым");

        var patient = new Patient(Guid.NewGuid(),fullName, phone, email, address, age, birthDay, trouble, description);
        return Result.Ok(patient);
    }
    public void AddAppointment(Appointment appointment)
    {
        _appointments.Add(appointment);
    }
}
