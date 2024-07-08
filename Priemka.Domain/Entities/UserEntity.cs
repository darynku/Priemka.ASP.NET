using Priemka.Domain.Common;
using Priemka.Domain.ValueObjects;

namespace Priemka.Domain.Entities;
public class UserEntity : Entity
{ 
    private UserEntity(FullName fullName, Email email, string passwordHash)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
    }

    public FullName FullName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;

    public static UserEntity Create(FullName fullName, Email email, string passwordHash)
    {
        return new UserEntity(fullName, email, passwordHash);
    }
}
