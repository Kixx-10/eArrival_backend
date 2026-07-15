using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMAC.Migrations
{
    /// <inheritdoc />
    public partial class ChangeDigitalDelcrationCollumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DigitalRecordUrl",
                table: "ArrivalApplication",
                newName: "GoodsRecordUrl");

            migrationBuilder.RenameColumn(
                name: "DigitalRecordFileName",
                table: "ArrivalApplication",
                newName: "GoodsRecordFileName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GoodsRecordUrl",
                table: "ArrivalApplication",
                newName: "DigitalRecordUrl");

            migrationBuilder.RenameColumn(
                name: "GoodsRecordFileName",
                table: "ArrivalApplication",
                newName: "DigitalRecordFileName");
        }
    }
}
