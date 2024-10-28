﻿// <auto-generated />
using System;
using MediConnectBackend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MediConnectBackend.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    partial class ApplicationDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MediConnectBackend.Models.Appointment", b =>
                {
                    b.Property<int>("AppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("AppointmentId"));

                    b.Property<DateTime>("AppointmentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("AppointmentStatus")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorId")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("PatientId")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.HasKey("AppointmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("MediConnectBackend.Models.PastAppointment", b =>
                {
                    b.Property<int>("PastAppointmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PastAppointmentId"));

                    b.Property<DateTime>("AppointmentDateTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CompletionDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("DoctorId")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<DateTime>("LastUpdatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("PatientId")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.HasKey("PastAppointmentId");

                    b.HasIndex("DoctorId");

                    b.HasIndex("PatientId");

                    b.ToTable("PastAppointments");
                });

            modelBuilder.Entity("MediConnectBackend.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("Email")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasDiscriminator().HasValue("User");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("Name")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("RoleId")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("Name")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("Value")
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("MediConnectBackend.Models.Doctor", b =>
                {
                    b.HasBaseType("MediConnectBackend.Models.User");

                    b.Property<string>("Availability")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("OfficeAddress")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("Specialty")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<int>("YearsOfExperience")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Doctor");
                });

            modelBuilder.Entity("MediConnectBackend.Models.Patient", b =>
                {
                    b.HasBaseType("MediConnectBackend.Models.User");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<DateOnly>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("EmergencyContactFirstName")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("EmergencyContactLastName")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("EmergencyContactPhoneNumber")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasMaxLength(191)
                        .HasColumnType("nvarchar(191)");

                    b.Property<DateTime>("RegistrationDate")
                        .HasColumnType("datetime2");

                    b.HasDiscriminator().HasValue("Patient");
                });

            modelBuilder.Entity("MediConnectBackend.Models.Appointment", b =>
                {
                    b.HasOne("MediConnectBackend.Models.Doctor", "Doctor")
                        .WithMany("Appointments")
                        .HasForeignKey("DoctorId");

                    b.HasOne("MediConnectBackend.Models.Patient", "Patient")
                        .WithMany("Appointments")
                        .HasForeignKey("PatientId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("MediConnectBackend.Models.PastAppointment", b =>
                {
                    b.HasOne("MediConnectBackend.Models.Doctor", "Doctor")
                        .WithMany("PastAppointments")
                        .HasForeignKey("DoctorId");

                    b.HasOne("MediConnectBackend.Models.Patient", "Patient")
                        .WithMany("PastAppointments")
                        .HasForeignKey("PatientId");

                    b.Navigation("Doctor");

                    b.Navigation("Patient");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MediConnectBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MediConnectBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MediConnectBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MediConnectBackend.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MediConnectBackend.Models.Doctor", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("PastAppointments");
                });

            modelBuilder.Entity("MediConnectBackend.Models.Patient", b =>
                {
                    b.Navigation("Appointments");

                    b.Navigation("PastAppointments");
                });
#pragma warning restore 612, 618
        }
    }
}
