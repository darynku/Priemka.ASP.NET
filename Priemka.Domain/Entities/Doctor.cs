﻿using FluentResults;
using Priemka.Domain.Common;
using Priemka.Domain.ValueObjects;

namespace Priemka.Domain.Entities;

public class Doctor : Entity
{
    private Doctor(
        Guid id,
        FullName fullName, 
        Phone phone,
        Email email, 
        Address address, 
        int age, 
        string speciality, 
        bool onVacation,
        IEnumerable<Achivments> achivments,
        IEnumerable<WorkShedule> workShedules) : base(id)
    {
        FullName = fullName;
        Phone = phone;
        Email = email;
        Address = address;
        Age = age;
        Speciality = speciality;
        _achivments = achivments.ToList();
        _workShedules = workShedules.ToList();
    }
    private Doctor() { }
    public FullName FullName { get; private set; } = null!;
    public Phone Phone { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public Address Address { get; private set; } = null!;
    
    public int Age { get; private set; }
    public string Speciality { get; private set; } = null!;
    
    //На отпуске или нет
    public bool OnVacation { get; private set; } 

    private readonly List<Achivments> _achivments = [];
    public IReadOnlyList<Achivments> Achivments => _achivments;
    
    private readonly List<WorkShedule> _workShedules = [];
    public IReadOnlyList<WorkShedule> WorkShedules => _workShedules;

    public static Result<Doctor> Create(
        Guid id,
        FullName fullName,
        Phone phone,
        Email email,
        Address address,
        int age,
        string speciality,
        bool onVacation,
        IEnumerable<Achivments> achivments,
        IEnumerable<WorkShedule> workShedules)
    {
        if (age < 16)
            return Result.Fail("Слишком молод");
        if (string.IsNullOrWhiteSpace(speciality))
            return Result.Fail("Название специальности не может быть пустым");

        var doctor = new Doctor(
            id,  
            fullName, 
            phone,
            email,
            address,
            age,
            speciality,
            onVacation, 
            achivments,
            workShedules);

        return Result.Ok(doctor);
    }

    public Result UpdateDoctor(string speciality, Address address, Phone phoneNumber, Email email)
    {
        var addressResult = Address.Create(address.City, address.Street);
        if (addressResult.IsFailed) return Result.Fail("Неправльный ввод данных (адрес)");

        var phoneResult = Phone.Create(phoneNumber.Number); 
        if (phoneResult.IsFailed) return Result.Fail("Неправльный ввод данных (телефон)");

        var emailResult = Email.Create(email.Value);
        if (emailResult.IsFailed) return Result.Fail("Неправльный ввод данных (почта)");

        Speciality = speciality;
        Address = addressResult.Value;
        Phone = phoneResult.Value;
        Email = emailResult.Value;

        return Result.Ok();
    }
}