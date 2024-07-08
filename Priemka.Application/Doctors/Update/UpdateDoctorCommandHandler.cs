using FluentResults;
using MediatR;
using Priemka.Domain.Interfaces;
using Priemka.Domain.ValueObjects;
namespace Priemka.Application.Doctors.Update
{
    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Result>
    {
        private readonly IDoctorsRepository _doctorsRepository;
        public UpdateDoctorCommandHandler(IDoctorsRepository doctorsRepository)
        {
            _doctorsRepository = doctorsRepository;
        }

        public async Task<Result> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctor = await _doctorsRepository.GetById(request.Id, cancellationToken);
            if (doctor is null)
                return Result.Fail($"Доктор с таким ID не найден {request.Id}");

            var addressResult = Address.Create(request.Street, request.City);
            if(addressResult.IsFailed) return addressResult.ToResult();

            var phoneResult = Phone.Create(request.Number);
            if(phoneResult.IsFailed) return phoneResult.ToResult();

            var emailResult = Email.Create(request.Email);    
            if(emailResult.IsFailed) return emailResult.ToResult();

            var result = doctor.UpdateDoctor(request.Speciality, addressResult.Value, phoneResult.Value, emailResult.Value);

            await _doctorsRepository.SaveChangeAsync(cancellationToken);

            return Result.Ok();

        }
    }
}
