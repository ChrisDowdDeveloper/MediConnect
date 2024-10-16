using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediConnectBackend.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePastAppointmentsColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDate",
                table: "PastAppointments");

            migrationBuilder.DropColumn(
                name: "AppointmentDuration",
                table: "PastAppointments");

            migrationBuilder.DropColumn(
                name: "AppointmentTime",
                table: "PastAppointments");

            migrationBuilder.DropColumn(
                name: "PastAppointmentStatus",
                table: "PastAppointments");

            migrationBuilder.RenameColumn(
                name: "DoctorNotes",
                table: "PastAppointments",
                newName: "Notes");

            migrationBuilder.AddColumn<DateTime>(
                name: "AppointmentDateTime",
                table: "PastAppointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CompletionDate",
                table: "PastAppointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "PastAppointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "LastUpdatedDate",
                table: "PastAppointments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppointmentDateTime",
                table: "PastAppointments");

            migrationBuilder.DropColumn(
                name: "CompletionDate",
                table: "PastAppointments");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "PastAppointments");

            migrationBuilder.DropColumn(
                name: "LastUpdatedDate",
                table: "PastAppointments");

            migrationBuilder.RenameColumn(
                name: "Notes",
                table: "PastAppointments",
                newName: "DoctorNotes");

            migrationBuilder.AddColumn<DateOnly>(
                name: "AppointmentDate",
                table: "PastAppointments",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<int>(
                name: "AppointmentDuration",
                table: "PastAppointments",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<TimeOnly>(
                name: "AppointmentTime",
                table: "PastAppointments",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "PastAppointmentStatus",
                table: "PastAppointments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
