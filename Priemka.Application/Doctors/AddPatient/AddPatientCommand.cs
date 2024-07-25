using FluentResults;
using MediatR;

namespace Priemka.Application.Doctors.AddPatient
{
    public record AddPatientCommand(
        Guid DoctorId,
        string FirstName, 
        string LastName,
        string Email,
        string Phone,
        string City,
        string Street,
        DateTime BirthDate,
        int Age,
        string Trouble,
        string Description) : IRequest<Result<Guid>>;
}
