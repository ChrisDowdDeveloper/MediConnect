using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MediConnectBackend.Migrations
{
    /// <inheritdoc />
    public partial class AdjustDeleteBehaviorForTimeSlot : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_AspNetUsers_DoctorId",
                table: "TimeSlots");

            migrationBuilder.AddColumn<int>(
                name: "AvailabilityId",
                table: "TimeSlots",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TimeSlots_AvailabilityId",
                table: "TimeSlots",
                column: "AvailabilityId");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_AspNetUsers_DoctorId",
                table: "TimeSlots",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_Availabilities_AvailabilityId",
                table: "TimeSlots",
                column: "AvailabilityId",
                principalTable: "Availabilities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_AspNetUsers_DoctorId",
                table: "TimeSlots");

            migrationBuilder.DropForeignKey(
                name: "FK_TimeSlots_Availabilities_AvailabilityId",
                table: "TimeSlots");

            migrationBuilder.DropIndex(
                name: "IX_TimeSlots_AvailabilityId",
                table: "TimeSlots");

            migrationBuilder.DropColumn(
                name: "AvailabilityId",
                table: "TimeSlots");

            migrationBuilder.AddForeignKey(
                name: "FK_TimeSlots_AspNetUsers_DoctorId",
                table: "TimeSlots",
                column: "DoctorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
