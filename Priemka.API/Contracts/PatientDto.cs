namespace Priemka.API.Contracts
{
    public record PatientDto
    {
        public Guid Id { get; init; }
        public string FirsName { get; init; } 
        public string LastName {  get; init; }
        public int Age { get; init; }
        public string Phone {  get; init; }
        public IEnumerable<AppointmentDto>? Appointments { get; init; }
    }
}
