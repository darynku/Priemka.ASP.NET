using FluentResults;
using Priemka.Domain.Entities;

namespace Priemka.Domain.Interfaces;

public interface IDoctorsRepository
{
    Task AddAsync(Doctor doctor, CancellationToken ct);
    Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken);
    Task<Result<Doctor>> GetById(Guid id, CancellationToken ct);

}