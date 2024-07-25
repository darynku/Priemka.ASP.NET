namespace Priemka.API.Contracts
{
    public class PatientDto
    {
        public Guid Id { get; init; }
        public string FirsName { get; init; } 
        public string LastName {  get; init; }
        public int Age { get; init; }
        public string Phone {  get; init; }
    }
}
