using FluentResults;
using Priemka.Domain.Common;
using Priemka.Domain.ValueObjects;

namespace Priemka.Domain.Entities;
public class UserEntity : Entity
{ 
    private UserEntity() { }
    private UserEntity(Email email, string passwordHash, Role role)
    {
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public Role Role { get; private set; } = null!;

    public static Result<UserEntity> CreateApplicationUser(Email email, string passwordHash)
    {
        if (passwordHash.Length < 5 && string.IsNullOrEmpty(passwordHash))
            return Result.Fail("Некоректный пароль");

        return new UserEntity(email, passwordHash, Role.ApplicationUser);
    }
    public static Result<UserEntity> CreateDoctor(Email email, string passwordHash)
    {
        if (passwordHash.Length < 5 && string.IsNullOrEmpty(passwordHash))
            return Result.Fail("Некоректный пароль");

        return new UserEntity(email, passwordHash, Role.Doctor);
    }
    public static Result<UserEntity> CreateAdmin(Email email, string passwordHash)
    {
        if (passwordHash.Length < 5 && string.IsNullOrEmpty(passwordHash))
            return Result.Fail("Некоректный пароль");

        return new UserEntity(email, passwordHash, Role.Admin);
    }
}
