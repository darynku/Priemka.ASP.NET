using FluentResults;
using MediatR;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;
using Priemka.Domain.ValueObjects;

namespace Priemka.Application.Doctors.Create
{
    public class CreateDoctorCommandHandler : IRequestHandler<CreateDoctorCommand, Result>
    {
        private readonly IDoctorsRepository _doctorsRepository;

        public CreateDoctorCommandHandler(IDoctorsRepository doctorsRepository)
        {
            _doctorsRepository = doctorsRepository;
        }

        public async Task<Result> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var fullNameResult = FullName.Create(request.FirstName, request.LastName);
            if (fullNameResult.IsFailed) return fullNameResult.ToResult();

            var phoneResult = Phone.Create(request.Number);
            if (phoneResult.IsFailed) return phoneResult.ToResult();

            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailed) return emailResult.ToResult();

            var addressResult = Address.Create(request.Street, request.City);
            if (addressResult.IsFailed) return addressResult.ToResult();

            var achivments = request.Achivments.Select(a => Achivments.Create(a.AchivmentName, a.AchivmentDate));
            if (achivments.Any(a => a.IsFailed)) return Result.Fail("Ошибка при добавлении достижений");
            var workShedule = request.WorkShedule.Select(a => WorkShedule.Create(a.DayOfWeek, a.ShiftStart, a.ShiftEnd));
            if (workShedule.Any(a => a.IsFailed)) return Result.Fail("Ошибка при создании графика работы");

            var doctor = Doctor.Create(
                Guid.NewGuid(),
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
            return Result.Ok();

        }
    }
}
