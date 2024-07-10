using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priemka.Domain.Entities;

namespace Priemka.Infrastructure.Configurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder.ToTable("application_user");

            builder.HasKey(x => x.Id);

            builder
                .HasOne<UserEntity>()
                .WithOne()
                .HasForeignKey<ApplicationUser>(x => x.Id);
        }
    }
}
