using BCrypt.Net;
using FluentResults;
using MediatR;
using Priemka.Application.DataAccess;
using Priemka.Application.Validator;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;
using Priemka.Domain.ValueObjects;
using System.Runtime.CompilerServices;

namespace Priemka.Application.Doctors.Create;

public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Result>
{
    private readonly IDoctorsRepository _doctorsRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IValidator<CreateDoctorCommand> _doctorValidator;
    public CreateDoctorCommandHandler(IDoctorsRepository doctorsRepository, IUnitOfWork unitOfWork, IUserRepository userRepository, IValidator<CreateDoctorCommand> doctorValidator)
    {
        _doctorsRepository = doctorsRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
        _doctorValidator = doctorValidator;
    }

    public async Task<Result> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _doctorValidator.Validate(request, cancellationToken);
        if (validationResult.IsFailed) return validationResult;

        var password = BCrypt.Net.BCrypt.HashPassword(request.Password);
        var emailResult = Email.Create(request.Email);

        var user = UserEntity.CreateDoctor(emailResult.Value, password);
        await _userRepository.AddAsync(user.Value, cancellationToken);

        if (user.IsFailed)
            return Result.Fail($"Ошибка при создании пользователя: {user.Value.Email}");

        var fullNameResult = FullName.Create(request.FirstName, request.LastName);
        var phoneResult = Phone.Create(request.Number);
        var addressResult = Address.Create(request.Street, request.City);
        var achivments = request.Achivments.Select(a => Achivments.Create(a.AchivmentName, a.AchivmentDate));
        var workShedule = request.WorkShedule.Select(a => WorkShedule.Create(a.DayOfWeek, a.ShiftStart, a.ShiftEnd));

        var doctor = Doctor.Create(
            user.Value.Id,
            fullNameResult.Value,
            phoneResult.Value,
            emailResult.Value,
            addressResult.Value,
            request.Age,
            request.Speciality,
            request.onVacation,
            achivments.Select(a => a.Value),
            workShedule.Select(w => w.Value));

        if (doctor.IsFailed)
            return doctor.ToResult();

        await _doctorsRepository.AddAsync(doctor.Value, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok();

    }
}
