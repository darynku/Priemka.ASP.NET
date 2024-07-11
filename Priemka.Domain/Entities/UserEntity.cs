using FluentResults;
using Priemka.Domain.Common;
using Priemka.Domain.ValueObjects;

namespace Priemka.Domain.Entities;
public class UserEntity : Entity
{ 
    private UserEntity() { }
    private UserEntity(FullName fullName, Email email, string passwordHash, Role role)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public FullName FullName { get; private set; } = null!;
    public Email Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public Role Role { get; private set; } = null!;

    public static Result<UserEntity> CreateApplicationUser(FullName fullName, Email email, string passwordHash)
    {
        if (passwordHash.Length < 5 && string.IsNullOrEmpty(passwordHash))
            return Result.Fail("Некоректный пароль");

        return new UserEntity(fullName, email, passwordHash, Role.ApplicationUser);
    }
    public static Result<UserEntity> CreateDoctor(FullName fullName, Email email, string passwordHash)
    {
        if (passwordHash.Length < 5 && string.IsNullOrEmpty(passwordHash))
            return Result.Fail("Некоректный пароль");

        return new UserEntity(fullName, email, passwordHash, Role.Doctor);
    }
    public static Result<UserEntity> CreateAdmin(FullName fullName, Email email, string passwordHash)
    {
        if (passwordHash.Length < 5 && string.IsNullOrEmpty(passwordHash))
            return Result.Fail("Некоректный пароль");

        return new UserEntity(fullName, email, passwordHash, Role.Admin);
    }
}
