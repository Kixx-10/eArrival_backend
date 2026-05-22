using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MMAC.Migrations
{
    /// <inheritdoc />
    public partial class CreateAddressAndNRC : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NRC_StateRegion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    IdCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    CodeMM = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    SystemUse = table.Column<string>(type: "char(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedIPAddr = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedIPAddr = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NRC_StateRegion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateRegion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    IdCode = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameMM = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SystemUse = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateRegion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NRC_Township",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    IdCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    CodeMM = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    NRC_SRId = table.Column<int>(type: "integer", nullable: false),
                    SystemUse = table.Column<string>(type: "char(1)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedUser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    CreatedIPAddr = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedUser = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    UpdatedIPAddr = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NRC_Township", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NRC_Township_NRC_StateRegion_NRC_SRId",
                        column: x => x.NRC_SRId,
                        principalTable: "NRC_StateRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "District",
                columns: table => new
                {
                    DistrictId = table.Column<int>(type: "integer", nullable: false),
                    SRId = table.Column<int>(type: "integer", nullable: false),
                    IdCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameMM = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SystemUse = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_District", x => x.DistrictId);
                    table.ForeignKey(
                        name: "FK_District_StateRegion_SRId",
                        column: x => x.SRId,
                        principalTable: "StateRegion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Township",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    DistrictId = table.Column<int>(type: "integer", nullable: false),
                    IdCode = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    NameMM = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    SystemUse = table.Column<string>(type: "char(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Township", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Township_District_DistrictId",
                        column: x => x.DistrictId,
                        principalTable: "District",
                        principalColumn: "DistrictId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_District_SRId",
                table: "District",
                column: "SRId");

            migrationBuilder.CreateIndex(
                name: "IX_NRC_Township_NRC_SRId",
                table: "NRC_Township",
                column: "NRC_SRId");

            migrationBuilder.CreateIndex(
                name: "IX_Township_DistrictId",
                table: "Township",
                column: "DistrictId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NRC_Township");

            migrationBuilder.DropTable(
                name: "Township");

            migrationBuilder.DropTable(
                name: "NRC_StateRegion");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "StateRegion");
        }
    }
}
