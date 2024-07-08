using Priemka.Domain.Common;
using Priemka.Domain.ValueObjects;
using FluentResults;
namespace Priemka.Domain.Entities;

public class Patient : Entity
{
    private Patient(
        Guid id,
        FullName fullName, 
        Phone phone,
        Email email,
        Address address,
        DateTime birthDay,
        string trouble,
        string description) : base(id)
    {
        FullName = fullName;
        Phone = phone;
        Email = email;
        Address = address;
        BirthDay = birthDay;
        Age = CalculateAge(birthDay);
        Trouble = trouble;
        Description = description;
    }
    
    private Patient() { }
    public FullName FullName { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    
    public DateTime BirthDay { get; private set; }
    public int Age { get; private set; }

    public string Trouble { get; private set; } = null!;
    public string Description { get; private set; } = null!;
    
    public static Result<Patient> Create(
        Guid id, 
        FullName fullName, 
        Phone phone,
        Email email,
        Address address,
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

        var patient = new Patient(id, fullName, phone, email, address, birthDay, trouble, description);
        return Result.Ok(patient);
    }

    private static int CalculateAge(DateTime birthDay)
    {
        var age = DateTime.Today.Year - birthDay.Year;
        if (birthDay.Date > DateTime.Today.AddYears(-age)) 
            age--;
        return age;
    }
}
