using FluentResults;
using Priemka.Application.Validator;
using Priemka.Domain.Interfaces;
using Priemka.Domain.ValueObjects;

namespace Priemka.Application.Doctors.Create
{
    public class CreateDoctorCommandVaildator : IValidator<CreateDoctorCommand>
    {
        private readonly IUserRepository _userRepository;

        public CreateDoctorCommandVaildator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result> Validate(CreateDoctorCommand request, CancellationToken cancellationToken)
        {
            var password = BCrypt.Net.BCrypt.HashPassword(request.Password);
            if (string.IsNullOrEmpty(password)) return Result.Fail("Password cannot be empty");

            var emailResult = Email.Create(request.Email);
            if (emailResult.IsFailed) return emailResult.ToResult();

            // Проверка, существует ли пользователь с таким email
            var existingUser = await _userRepository.GetByEmail(emailResult.Value, cancellationToken);
            if (existingUser.IsSuccess)
                return Result.Fail("Пользователь с таким email уже существует");

            var fullNameResult = FullName.Create(request.FirstName, request.LastName);
            if (fullNameResult.IsFailed) return fullNameResult.ToResult();

            var phoneResult = Phone.Create(request.Number);
            if (phoneResult.IsFailed) return phoneResult.ToResult();

            var addressResult = Address.Create(request.Street, request.City);
            if (addressResult.IsFailed) return addressResult.ToResult();

            var achivments = request.Achivments.Select(a => Achivments.Create(a.AchivmentName, a.AchivmentDate));
            if (achivments.Any(a => a.IsFailed)) return Result.Fail("Ошибка при добавлении достижений");

            var workShedule = request.WorkShedule.Select(a => WorkShedule.Create(a.DayOfWeek, a.ShiftStart, a.ShiftEnd));
            if (workShedule.Any(a => a.IsFailed)) return Result.Fail("Ошибка при создании графика работы");

            return Result.Ok();
        }
    }
}
