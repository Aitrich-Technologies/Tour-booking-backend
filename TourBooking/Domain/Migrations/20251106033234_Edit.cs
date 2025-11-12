using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class Edit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestedBy",
                table: "EditRequests");

            migrationBuilder.AddColumn<Guid>(
                name: "RequestedByUserId",
                table: "EditRequests",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "RespondedAt",
                table: "EditRequests",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EditRequests_BookingId",
                table: "EditRequests",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_EditRequests_TourBookingForms_BookingId",
                table: "EditRequests",
                column: "BookingId",
                principalTable: "TourBookingForms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EditRequests_TourBookingForms_BookingId",
                table: "EditRequests");

            migrationBuilder.DropIndex(
                name: "IX_EditRequests_BookingId",
                table: "EditRequests");

            migrationBuilder.DropColumn(
                name: "RequestedByUserId",
                table: "EditRequests");

            migrationBuilder.DropColumn(
                name: "RespondedAt",
                table: "EditRequests");

            migrationBuilder.AddColumn<string>(
                name: "RequestedBy",
                table: "EditRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
