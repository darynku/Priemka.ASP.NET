
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priemka.Domain.Entities;

namespace Priemka.Infrastructure.Configurations
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("admins");

            builder.HasKey(a => a.Id);

            builder
                .HasOne<UserEntity>()
                .WithOne()
                .HasForeignKey<Admin>(a => a.Id);
        }
    }
}
