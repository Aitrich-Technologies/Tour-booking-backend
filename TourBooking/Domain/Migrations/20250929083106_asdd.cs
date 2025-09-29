using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class asdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "Destinations");

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Destinations",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Destinations");

            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "Destinations",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
