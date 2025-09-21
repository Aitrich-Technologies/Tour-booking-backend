using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantInformations_TourBookingForms_TourBookingFormId",
                table: "ParticipantInformations");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantInformations_TourBookingFormId",
                table: "ParticipantInformations");

            migrationBuilder.DropColumn(
                name: "TourBookingFormId",
                table: "ParticipantInformations");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParticipantInformations_TourBookingForms_LeadId",
                table: "ParticipantInformations");

            migrationBuilder.DropIndex(
                name: "IX_ParticipantInformations_LeadId",
                table: "ParticipantInformations");

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
    }
}
