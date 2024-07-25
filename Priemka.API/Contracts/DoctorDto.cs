using Priemka.Domain.Entities;

namespace Priemka.API.Contracts
{
    public record DoctorDto
    {
        public string? Name { get; init; } 
        public string? LastName { get; init; }
        public string? Phone { get; init; }
        public bool OnVacation { get; init; }
        public IEnumerable<Patient>? Patients { get; init; }
    }
}
