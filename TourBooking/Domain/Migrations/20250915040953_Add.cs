using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Add : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantInformations_TourBookingForms_LeadId",
                table: "ParticipantInformations");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantInformations_LeadId",
                table: "ParticipantInformations");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ParticipantInformations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "NEWSEQUENTIALID()",
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "ParticipantInformations",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "TourBookingFormId",
                table: "ParticipantInformations",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantInformations_TourBookingFormId",
                table: "ParticipantInformations",
                column: "TourBookingFormId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantInformations_TourBookingForms_TourBookingFormId",
                table: "ParticipantInformations",
                column: "TourBookingFormId",
                principalTable: "TourBookingForms",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantInformations_TourBookingForms_TourBookingFormId",
                table: "ParticipantInformations");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantInformations_TourBookingFormId",
                table: "ParticipantInformations");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "ParticipantInformations");

            migrationBuilder.DropColumn(
                name: "TourBookingFormId",
                table: "ParticipantInformations");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "ParticipantInformations",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldDefaultValueSql: "NEWSEQUENTIALID()");

            migrationBuilder.CreateIndex(
                name: "IX_ParticipantInformations_LeadId",
                table: "ParticipantInformations",
                column: "LeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParticipantInformations_TourBookingForms_LeadId",
                table: "ParticipantInformations",
                column: "LeadId",
                principalTable: "TourBookingForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
