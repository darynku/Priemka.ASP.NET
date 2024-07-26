using FluentResults;
using Priemka.Domain.Entities;
using Priemka.Domain.Interfaces;

namespace Priemka.Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken)
        {
            await _context.Appointments.AddAsync(appointment, cancellationToken);
        }

    }
}
