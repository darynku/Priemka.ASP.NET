using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priemka.Domain.Entities;

namespace Priemka.Infrastructure.Configurations
{
    public class AppointmentConfiguration : IEntityTypeConfiguration<Appointment>
    {
        public void Configure(EntityTypeBuilder<Appointment> builder)
        {
            builder.ToTable("appointment");
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Summary).HasColumnName("summary");
            builder.Property(a => a.Description).HasColumnName("description");
            builder.OwnsMany(a => a.Medications, m =>
            {
                m.ToJson();
                m.Property(x => x.Dosage).HasColumnName("dosage");
                m.Property(x => x.Name).HasColumnName("name");
            });
        }
    }
}
