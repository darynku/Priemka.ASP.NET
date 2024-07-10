using Priemka.Domain.Common;
using Priemka.Domain.ValueObjects;

namespace Priemka.Domain.Entities;
public class UserEntity : Entity
{ 
    private UserEntity(FullName fullName, Email email, string passwordHash, Role role)
    {
        FullName = fullName;
        Email = email;
        PasswordHash = passwordHash;
        Role = role;
    }

    public FullName FullName { get; private set; } 
    public Email Email { get; private set; } 
    public string PasswordHash { get; private set; }
    public Role Role { get; private set; } 

    public static UserEntity CreateApplicationUser(FullName fullName, Email email, string passwordHash)
    {
        return new UserEntity(fullName, email, passwordHash, Role.ApplicationUser);
    }
    public static UserEntity CreateDoctor(FullName fullName, Email email, string passwordHash)
    {
        return new UserEntity(fullName, email, passwordHash, Role.Doctor);
    }
    public static UserEntity CreateAdmin(FullName fullName, Email email, string passwordHash)
    {
        return new UserEntity(fullName, email, passwordHash, Role.Admin);
    }
}
