using FluentResults;
using MediatR;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;

namespace Priemka.Application.Doctors.Get
{
    public class GetDoctorQueryHandler : IRequestHandler<GetDoctorQuery, Result<Doctor>>
    {
        private readonly IDoctorsRepository _repository;

        public GetDoctorQueryHandler(IDoctorsRepository repository)
        {
            _repository = repository;
        }

        public async Task<Result<Doctor>> Handle(GetDoctorQuery request, CancellationToken cancellationToken)
        {
            var doctors = await _repository.GetAllDoctorsAsync(cancellationToken);
            
            return Result.Ok();
        }
    }
}
