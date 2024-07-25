using FluentResults;
using MediatR;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;

namespace Priemka.Application.Patients
{
    public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, Result<IEnumerable<Patient>>>
    {
        private readonly IPatientRepository _patientRepository;

        public GetPatientsQueryHandler(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        public async Task<Result<IEnumerable<Patient>>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
        {
            var patients = await _patientRepository.GetAll(cancellationToken);
            if (patients.IsFailed)
                return patients.ToResult();
            return Result.Ok(patients.Value);
        }
    }
}
