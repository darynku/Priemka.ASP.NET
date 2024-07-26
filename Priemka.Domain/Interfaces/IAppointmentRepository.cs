using FluentResults;
using Priemka.Domain.Entities;

namespace Priemka.Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task AddAsync(Appointment appointment, CancellationToken cancellationToken); 
    }
}
