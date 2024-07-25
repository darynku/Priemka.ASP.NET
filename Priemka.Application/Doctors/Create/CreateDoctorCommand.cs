using FluentResults;
using MediatR;
using Priemka.Domain.Entities;
using Priemka.Domain.ValueObjects;

namespace Priemka.Application.Doctors.Create
{
    public record CreateDoctorCommand(
        string Password,
        string FirstName, 
        string LastName,
        string City, 
        string Street,
        string Email, 
        string Number,
        string Speciality,
        int Age,
        bool onVacation,
        IEnumerable<Achivments> Achivments,
        IEnumerable<WorkShedule> WorkShedule) : IRequest<Result>;

}
