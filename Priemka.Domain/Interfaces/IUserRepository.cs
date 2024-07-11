using FluentResults;
using Priemka.Domain.Entities;

namespace Priemka.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user, CancellationToken cancellationToken);
        Task<Result<UserEntity>> GetByEmail(string Email, CancellationToken cancellationToken);

    }
}
