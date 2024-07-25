using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using Priemka.Domain.Entities;
using Priemka.Domain.ValueObjects;

namespace Priemka.Infrastructure.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("doctors");

            builder.HasKey(d => d.Id);

            builder
                .HasOne<UserEntity>()
                .WithOne()
                .HasForeignKey<Doctor>(d => d.Id);

            builder.HasMany<Appointment>().WithMany();

            builder.HasMany(d => d.Patients).WithOne();

            builder.Property(d => d.Age).IsRequired();
            builder.Property(d => d.Speciality).HasColumnName("speciality").IsRequired();
            builder.Property(d => d.OnVacation).HasColumnName("on_vacation").IsRequired();

            builder.ComplexProperty(d => d.FullName, f =>
            {
                f.Property(a => a.FirstName)
                    .HasColumnName("first_name")
                    .IsRequired();

                f.Property(a => a.LastName)
                    .HasColumnName("last_name")
                    .IsRequired();
            });

            builder.ComplexProperty(d => d.Phone, p =>
            {
                p.Property(ph => ph.Number)
                    .HasColumnName("phone")
                    .IsRequired();
            });

            builder.ComplexProperty(d => d.Email, e =>
            {
                e.Property(em => em.Value)
                    .HasColumnName("email")
                    .IsRequired();
            });

            builder.ComplexProperty(d => d.Address, a =>
            {
                a.Property(ad => ad.City)
                    .HasColumnName("city")
                    .IsRequired();

                a.Property(ad => ad.Street)
                    .HasColumnName("street")
                    .IsRequired();

            });

            //builder.OwnsOne(d => d.DoctorTicket, ticket =>
            //{
            //    ticket.ToJson();

            //    ticket.Property(t => t.Summary)
            //        .HasColumnName("summary")
            //        .IsRequired();

            //    ticket.Property(t => t.Description)
            //        .HasColumnName("description")
            //        .IsRequired();

            //    ticket.Property(t => t.AppointmentDate)
            //        .HasColumnName("appointment_date")
            //        .IsRequired();

            //    ticket.OwnsMany(t => t.Medications, medication =>
            //    {
            //        medication.ToJson();
            //        medication.Property(m => m.Name).IsRequired();
            //        medication.Property(m => m.Dosage).IsRequired();
            //    });
            //});
            builder.OwnsMany(
                d => d.Achivments, a =>
            {
                a.Property(a => a.AchivmentName);
                a.Property(a => a.AchivmentDate);
                a.ToJson();
            });

            builder.Metadata.FindNavigation(nameof(Doctor.Achivments))!.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.OwnsMany(
                d => d.WorkShedules, 
                w =>
            {
                w.Property(w => w.ShiftStart);
                w.Property(w => w.ShiftEnd);
                w.Property(w => w.DayOfWeek);
                w.ToJson();
            });
            builder.Metadata.FindNavigation(nameof(Doctor.WorkShedules))!.SetPropertyAccessMode(PropertyAccessMode.Field);

        }
    }
}
