using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priemka.Domain.Entities;

namespace Priemka.Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable("users");

            builder.HasKey(u => u.Id);

            builder.ComplexProperty(u => u.FullName, builder =>
            {
                builder.Property(a => a.FirstName).HasColumnName("first_name");
                builder.Property(a => a.LastName).HasColumnName("last_name");
            });

            builder.ComplexProperty(a => a.Email, builder =>
            {
                builder.Property(e => e.Value).HasColumnName("email");
            });

            builder.ComplexProperty(u => u.Role, builder =>
            {
                builder.Property(a => a.Name).HasColumnName("role");
                builder.Property(a => a.Permissions).HasColumnName("permissions");
            });

            builder.Property(u => u.PasswordHash).IsRequired();
        }
    }
}
