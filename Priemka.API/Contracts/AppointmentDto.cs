namespace Priemka.API.Contracts
{
    public record AppointmentDto
    {
        public string Summary { get; init; }
        public string Description { get; init; }
        public DateTime AppoinmentDate { get; init; }
    }
}
