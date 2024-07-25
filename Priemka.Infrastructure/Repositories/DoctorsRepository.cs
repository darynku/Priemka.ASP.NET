using FluentResults;
using Microsoft.EntityFrameworkCore;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;

namespace Priemka.Infrastructure.Repositories
{
    public class DoctorsRepository : IDoctorsRepository
    {
        private readonly ApplicationDbContext _context;

        public DoctorsRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Doctor>> GetAllDoctorsAsync(CancellationToken cancellationToken)
        {
            var doctors = await _context.Doctors
                .Include(d => d.Patients)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return doctors;
        }
        public async Task<Result<Doctor>> GetById(Guid id, CancellationToken ct)
        {
            var doctor = await _context.Doctors
                .Include(d => d.Patients)
                .FirstOrDefaultAsync(d => d.Id == id, ct);
            if (doctor is null)
                return Result.Fail($"Доктор с такиm id не найден: {id}");
                
            return Result.Ok(doctor);
        }
        public async Task AddAsync(Doctor doctor, CancellationToken ct)
        {
            await _context.Doctors.AddAsync(doctor, ct);
        }
    }
}
