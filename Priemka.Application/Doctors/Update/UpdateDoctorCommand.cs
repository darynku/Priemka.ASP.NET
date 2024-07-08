using FluentResults;
using MediatR;

namespace Priemka.Application.Doctors.Update
{
    public record UpdateDoctorCommand(
        Guid Id,
        string Speciality,
        string City,
        string Street,
        string Number,
        string Email) : IRequest<Result>;
}
