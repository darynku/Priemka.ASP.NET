using FluentResults;
using MediatR;
using Priemka.Domain.Entities;

namespace Priemka.Application.Patients
{
    public record GetPatientsQuery() : IRequest<Result<IEnumerable<Patient>>>;
}
