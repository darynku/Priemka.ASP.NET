using FluentResults;
using Priemka.Domain.Entities;

namespace Priemka.Domain.Interfaces
{
    public interface IJwtProvider
    {
       Result<string> Generate(UserEntity user);
    }
}
