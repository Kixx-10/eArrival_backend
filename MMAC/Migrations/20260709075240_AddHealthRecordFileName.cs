using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMAC.Migrations
{
    /// <inheritdoc />
    public partial class AddHealthRecordFileName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "HealthRecordFileName",
                table: "ArrivalApplication",
                type: "varchar(255)",
                maxLength: 255,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HealthRecordFileName",
                table: "ArrivalApplication");
        }
    }
}
