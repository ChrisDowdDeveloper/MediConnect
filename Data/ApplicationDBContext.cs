using System;
using System.Linq;
using MediConnectBackend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MediConnectBackend.Data
{
    public class ApplicationDBContext : IdentityDbContext<User>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options) { }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<PastAppointment> PastAppointments { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<Availability> Availabilities { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Reduce the length of indexed string columns for MySQL compatibility
            builder.Entity<IdentityRole>(entity =>
            {
                entity.Property(r => r.Name).HasMaxLength(191);
                entity.Property(r => r.NormalizedName).HasMaxLength(191);
            });

            builder.Entity<User>(entity =>
            {
                entity.Property(u => u.UserName).HasMaxLength(191);
                entity.Property(u => u.NormalizedUserName).HasMaxLength(191);
                entity.Property(u => u.Email).HasMaxLength(191);
                entity.Property(u => u.NormalizedEmail).HasMaxLength(191);
            });

            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.Property(l => l.LoginProvider).HasMaxLength(191);
                entity.Property(l => l.ProviderKey).HasMaxLength(191);
            });

            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.Property(t => t.LoginProvider).HasMaxLength(191);
                entity.Property(t => t.Name).HasMaxLength(191);
            });

            // Apply a general string length limitation for all other string fields
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var properties = entityType.GetProperties()
                    .Where(p => p.ClrType == typeof(string) && p.GetMaxLength() == null);

                foreach (var property in properties)
                {
                    property.SetMaxLength(191); // Limit all unspecified string properties to 191 characters
                }
            }

            // Define relationships between entities
            builder.Entity<Appointment>()
                .HasOne(a => a.Doctor)
                .WithMany(d => d.Appointments)
                .HasForeignKey(a => a.DoctorId);

            builder.Entity<Appointment>()
                .HasOne(a => a.Patient)
                .WithMany(p => p.Appointments)
                .HasForeignKey(a => a.PatientId);

            builder.Entity<PastAppointment>()
                .HasOne(pa => pa.Doctor)
                .WithMany(d => d.PastAppointments)
                .HasForeignKey(pa => pa.DoctorId);

            builder.Entity<PastAppointment>()
                .HasOne(pa => pa.Patient)
                .WithMany(p => p.PastAppointments)
                .HasForeignKey(pa => pa.PatientId);

            // Add configurations for TimeSlot entity
             builder.Entity<TimeSlot>(entity =>
            {
                entity.HasKey(ts => ts.Id);

                entity.Property(ts => ts.DoctorId)
                      .IsRequired();

                entity.Property(ts => ts.StartDateTime)
                      .IsRequired();

                entity.Property(ts => ts.EndDateTime)
                      .IsRequired();

                entity.Property(ts => ts.IsBooked)
                      .IsRequired();

                // Configure relationships
                entity.HasOne(ts => ts.Doctor)
                      .WithMany(d => d.TimeSlots)
                      .HasForeignKey(ts => ts.DoctorId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Configure optional relationship with Appointment
                entity.HasOne(ts => ts.Appointment)
                      .WithOne(a => a.TimeSlot)
                      .HasForeignKey<Appointment>(a => a.TimeSlotId)
                      .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Availability entity
            builder.Entity<Availability>(entity =>
            {
                entity.HasKey(a => a.Id);

                entity.Property(a => a.DoctorId)
                      .IsRequired();

                entity.Property(a => a.DayOfWeek)
                      .IsRequired();

                entity.Property(a => a.StartTime)
                      .IsRequired();

                entity.Property(a => a.EndTime)
                      .IsRequired();

                entity.Property(a => a.IsRecurring)
                      .IsRequired();

                // Configure relationships
                entity.HasOne(a => a.Doctor)
                      .WithMany(d => d.Availabilities)
                      .HasForeignKey(a => a.DoctorId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
