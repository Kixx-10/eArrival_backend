using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMAC.Migrations
{
    /// <inheritdoc />
    public partial class AddColumnAndChangeColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Traveller_Country_CountryOfBirthCode",
                table: "Traveller");

            migrationBuilder.RenameColumn(
                name: "CountryOfBirthCode",
                table: "Traveller",
                newName: "PlaceOfBirthCode");

            migrationBuilder.RenameIndex(
                name: "IX_Traveller_CountryOfBirthCode",
                table: "Traveller",
                newName: "IX_Traveller_PlaceOfBirthCode");

            migrationBuilder.AddColumn<string>(
                name: "NationalityCode",
                table: "Traveller",
                type: "varchar(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Traveller_IssuedCountryCode",
                table: "Traveller",
                column: "IssuedCountryCode");

            migrationBuilder.CreateIndex(
                name: "IX_Traveller_NationalityCode",
                table: "Traveller",
                column: "NationalityCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Traveller_Country_IssuedCountryCode",
                table: "Traveller",
                column: "IssuedCountryCode",
                principalTable: "Country",
                principalColumn: "CountryISOAlpha3Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Traveller_Country_NationalityCode",
                table: "Traveller",
                column: "NationalityCode",
                principalTable: "Country",
                principalColumn: "CountryISOAlpha3Code",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Traveller_Country_PlaceOfBirthCode",
                table: "Traveller",
                column: "PlaceOfBirthCode",
                principalTable: "Country",
                principalColumn: "CountryISOAlpha3Code",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Traveller_Country_IssuedCountryCode",
                table: "Traveller");

            migrationBuilder.DropForeignKey(
                name: "FK_Traveller_Country_NationalityCode",
                table: "Traveller");

            migrationBuilder.DropForeignKey(
                name: "FK_Traveller_Country_PlaceOfBirthCode",
                table: "Traveller");

            migrationBuilder.DropIndex(
                name: "IX_Traveller_IssuedCountryCode",
                table: "Traveller");

            migrationBuilder.DropIndex(
                name: "IX_Traveller_NationalityCode",
                table: "Traveller");

            migrationBuilder.DropColumn(
                name: "NationalityCode",
                table: "Traveller");

            migrationBuilder.RenameColumn(
                name: "PlaceOfBirthCode",
                table: "Traveller",
                newName: "CountryOfBirthCode");

            migrationBuilder.RenameIndex(
                name: "IX_Traveller_PlaceOfBirthCode",
                table: "Traveller",
                newName: "IX_Traveller_CountryOfBirthCode");

            migrationBuilder.AddForeignKey(
                name: "FK_Traveller_Country_CountryOfBirthCode",
                table: "Traveller",
                column: "CountryOfBirthCode",
                principalTable: "Country",
                principalColumn: "CountryISOAlpha3Code",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
