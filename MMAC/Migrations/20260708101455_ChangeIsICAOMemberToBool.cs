using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMAC.Migrations
{
    /// <inheritdoc />
    public partial class ChangeIsICAOMemberToBool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Country");

            migrationBuilder.AddColumn<bool>(
                name: "IsICAOMember",
                table: "Country",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsICAOMember",
                table: "Country");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "Country",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
