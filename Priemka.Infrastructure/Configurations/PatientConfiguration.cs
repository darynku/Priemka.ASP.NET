using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Priemka.Domain.Entities;

namespace Priemka.Infrastructure.Configurations
{
    public class PatientConfiguration : IEntityTypeConfiguration<Patient>
    {
        public void Configure(EntityTypeBuilder<Patient> builder)
        {
            builder.ToTable("patients");
            builder.HasKey(p => p.Id);

            builder.HasMany<Appointment>().WithMany();


            builder.Property(p => p.Age).HasColumnName("age");
            builder.Property(p => p.Trouble).HasColumnName("trouble");
            builder.Property(p => p.BirthDay).HasColumnName("birth_date");
            builder.Property(p => p.Description).HasColumnName("description");

            builder.ComplexProperty(p => p.FullName, f =>
            {
                f.Property(n => n.FirstName)
                .HasColumnName("firstName")
                .HasMaxLength(30)
                .IsRequired();

                f.Property(n => n.LastName)
                .HasColumnName("lastName")
                .HasMaxLength(30)
                .IsRequired();
            });

            builder.ComplexProperty(p => p.Phone, d =>
            {
                d.Property(ph => ph.Number)
                    .HasColumnName("phone")
                    .IsRequired();
            });

            builder.ComplexProperty(p => p.Email, e =>
            {
                e.Property(em => em.Value)
                    .HasColumnName("email")
                    .IsRequired();
            });

            builder.ComplexProperty(p => p.Address, a =>
            {
                a.Property(ad => ad.City)
                    .HasColumnName("city")
                    .IsRequired();

                a.Property(ad => ad.Street)
                    .HasColumnName("street")
                    .IsRequired();

            });
        }
    }
}
