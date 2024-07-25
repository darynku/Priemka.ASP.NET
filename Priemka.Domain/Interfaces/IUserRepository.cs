using FluentResults;
using Priemka.Domain.Entities;
using Priemka.Domain.ValueObjects;

namespace Priemka.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(UserEntity user, CancellationToken cancellationToken);
        Task<Result<UserEntity>> GetByEmail(Email email, CancellationToken cancellationToken);
        Task<Result<UserEntity>> GetEmailAsString(string email, CancellationToken cancellationToken);
    }
}
