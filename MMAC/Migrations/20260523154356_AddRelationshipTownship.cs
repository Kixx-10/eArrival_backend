using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMAC.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipTownship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ArrivalApplication_TownshipId",
                table: "ArrivalApplication",
                column: "TownshipId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArrivalApplication_Township_TownshipId",
                table: "ArrivalApplication",
                column: "TownshipId",
                principalTable: "Township",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArrivalApplication_Township_TownshipId",
                table: "ArrivalApplication");

            migrationBuilder.DropIndex(
                name: "IX_ArrivalApplication_TownshipId",
                table: "ArrivalApplication");
        }
    }
}
