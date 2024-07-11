using FluentResults;
using MediatR;
using Priemka.Application.DataAccess;
using Priemka.Domain.Interfaces;
using Priemka.Domain.ValueObjects;
namespace Priemka.Application.Doctors.Update
{
    public class UpdateDoctorCommandHandler : IRequestHandler<UpdateDoctorCommand, Result>
    {
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateDoctorCommandHandler(IDoctorsRepository doctorsRepository, IUnitOfWork unitOfWork)
        {
            _doctorsRepository = doctorsRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(UpdateDoctorCommand request, CancellationToken cancellationToken)
        {
            var doctorResult = await _doctorsRepository.GetById(request.Id, cancellationToken);
            var doctor = doctorResult.Value;

            var addressResult = Address.Create(request.Street, request.City);
            if(addressResult.IsFailed) return addressResult.ToResult();

            var phoneResult = Phone.Create(request.Number);
            if(phoneResult.IsFailed) return phoneResult.ToResult();

            var emailResult = Email.Create(request.Email);    
            if(emailResult.IsFailed) return emailResult.ToResult();

            var result = doctor.UpdateDoctor(request.Speciality, addressResult.Value, phoneResult.Value, emailResult.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Ok();

        }
    }
}
