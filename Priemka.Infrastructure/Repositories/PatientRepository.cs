using FluentResults;
using Microsoft.EntityFrameworkCore;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;
using static Priemka.Domain.Common.Permissions;

namespace Priemka.Infrastructure.Repositories;

public class PatientRepository : IPatientRepository
{
    private readonly ApplicationDbContext _context;

    public PatientRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Patient>> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var patient = await _context.Patient.FirstOrDefaultAsync(p => p.Email.Value == email, cancellationToken);
        if(patient is null)
            return Result.Fail($"Пациент с таким email не найден: {email}");
        return patient;
    }
    

    public async Task<Result<IEnumerable<Patient>>> GetAll(CancellationToken cancellationToken)
    {
        var parients = await _context.Patient.ToListAsync(cancellationToken);
        if(parients is null)
            return Result.Fail("Пациенты не найдены");
        return parients;
    }

    public async Task<Result<Patient>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var patient = await _context.Patient.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
        if (patient is null)
            return Result.Fail($"Пациент с таким id не найден: {id}");
        return patient;
    }
}
