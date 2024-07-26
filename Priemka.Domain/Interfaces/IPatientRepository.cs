using FluentResults;
using Priemka.Domain.Entities;

namespace Priemka.Domain.Interfaces;

public interface IPatientRepository
{
    Task AddAsync(Patient patient, CancellationToken ct);
    Task<Result<Patient>> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Result<Patient>> GetByEmailAsync(string email, CancellationToken cancellationToken);
    Task<Result<IEnumerable<Patient>>> GetAll(CancellationToken cancellationToken);

}
