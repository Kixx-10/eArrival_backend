using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMAC.Migrations
{
    /// <inheritdoc />
    public partial class ChnageAndRemovePlaceOfResidence : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Traveller_Country_PlaceOfResidenceCode",
                table: "Traveller");

            migrationBuilder.DropIndex(
                name: "IX_Traveller_PlaceOfResidenceCode",
                table: "Traveller");

            migrationBuilder.DropColumn(
                name: "PlaceOfResidenceCode",
                table: "Traveller");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Traveller",
                type: "varchar(50)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfResidence",
                table: "Traveller",
                type: "varchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlaceOfResidence",
                table: "Traveller");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "Traveller",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 30);

            migrationBuilder.AddColumn<string>(
                name: "PlaceOfResidenceCode",
                table: "Traveller",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Traveller_PlaceOfResidenceCode",
                table: "Traveller",
                column: "PlaceOfResidenceCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Traveller_Country_PlaceOfResidenceCode",
                table: "Traveller",
                column: "PlaceOfResidenceCode",
                principalTable: "Country",
                principalColumn: "CountryISOAlpha3Code",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
