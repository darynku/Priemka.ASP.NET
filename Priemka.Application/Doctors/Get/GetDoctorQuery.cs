using FluentResults;
using MediatR;
using Priemka.Domain.Entities;

namespace Priemka.Application.Doctors.Get
{
    public record GetDoctorQuery(Guid Id) : IRequest<Result<Doctor>>;
}
