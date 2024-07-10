using FluentResults;
using MediatR;
using Priemka.Domain.Entities;

namespace Priemka.Application.Doctors.Get
{
    public record GetDoctorQuery() : IRequest<Result<List<Doctor>>>;
}
