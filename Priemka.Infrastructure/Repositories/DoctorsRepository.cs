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
            var doctors = await _context.Doctors.AsNoTracking().ToListAsync(cancellationToken);
            return doctors;
        }
        public async Task<Doctor> GetById(Guid id, CancellationToken ct)
        {
            var doctor = await _context.Doctors.FirstOrDefaultAsync(d => d.Id == id, ct);
            return doctor;
        }
        public async Task AddAsync(Doctor doctor, CancellationToken ct)
        {
            await _context.Doctors.AddAsync(doctor, ct);
            await _context.SaveChangesAsync(ct);
        }

        public async Task SaveChangeAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
