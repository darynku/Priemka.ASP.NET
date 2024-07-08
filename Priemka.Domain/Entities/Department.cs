using Priemka.Domain.Common;
using FluentResults;
namespace Priemka.Domain.Entities;

public class Department : Entity
{ 
    private Department(
        Guid id,
        string name) : base(id)
    {
        Name = name;
    }
    private Department() { }
    
    public string Name { get; private set; } = null!;
    
    
    private readonly List<Doctor> _doctors = new();
    public IReadOnlyList<Doctor> Doctors => _doctors;



    public static Result<Department> Create(Guid id, string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Fail("Название отдела не может быть пустым");

        return new Department(id, name);
        
    }

    public void AddDoctor(Doctor doctor)
    {
        _doctors.Add(doctor);
    }
}
