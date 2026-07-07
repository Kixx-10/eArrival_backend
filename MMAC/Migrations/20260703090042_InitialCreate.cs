using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MMAC.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Country",
                columns: table => new
                {
                    CountryISOAlpha3Code = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    NameMM = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "CURRENT_DATE"),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Country", x => x.CountryISOAlpha3Code);
                });

            migrationBuilder.CreateTable(
                name: "ModeOfTravel",
                columns: table => new
                {
                    ModeOfTravelId = table.Column<int>(type: "integer", nullable: false),
                    ModeOfTravelName = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModeOfTravel", x => x.ModeOfTravelId);
                });

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
                name: "Traveller",
                columns: table => new
                {
                    TravellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    Gender = table.Column<string>(type: "character varying(1)", maxLength: 1, nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: false),
                    CountryOfBirthCode = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    Email = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Occupation = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    MobileNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    VisaNo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    NRC = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: true),
                    UID = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    FatherName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    PassportNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    IssuedCountryCode = table.Column<string>(type: "varchar(3)", maxLength: 3, nullable: false),
                    IssuedDate = table.Column<DateTime>(type: "date", nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "date", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traveller", x => x.TravellerId);
                    table.ForeignKey(
                        name: "FK_Traveller_Country_CountryOfBirthCode",
                        column: x => x.CountryOfBirthCode,
                        principalTable: "Country",
                        principalColumn: "CountryISOAlpha3Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PortOfArrival",
                columns: table => new
                {
                    PortOfArrivalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PortOfArrivalName = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    ModeOfTravelId = table.Column<int>(type: "integer", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PortOfArrival", x => x.PortOfArrivalId);
                    table.ForeignKey(
                        name: "FK_PortOfArrival_ModeOfTravel_ModeOfTravelId",
                        column: x => x.ModeOfTravelId,
                        principalTable: "ModeOfTravel",
                        principalColumn: "ModeOfTravelId",
                        onDelete: ReferentialAction.Cascade);
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
                name: "AuditLogs",
                columns: table => new
                {
                    LogId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LogTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TravellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    LogIPAddr = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Activity = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    Inputted = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.LogId);
                    table.ForeignKey(
                        name: "FK_AuditLogs_Traveller_TravellerId",
                        column: x => x.TravellerId,
                        principalTable: "Traveller",
                        principalColumn: "TravellerId",
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

            migrationBuilder.CreateTable(
                name: "ArrivalApplication",
                columns: table => new
                {
                    AppNo = table.Column<Guid>(type: "uuid", nullable: false),
                    ReferenceNo = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    TravellerId = table.Column<Guid>(type: "uuid", nullable: false),
                    AppStatus = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    ArrivalDate = table.Column<DateTime>(type: "date", nullable: false),
                    ModeOfTravelId = table.Column<int>(type: "integer", nullable: false),
                    PortOfArrivalId = table.Column<int>(type: "integer", nullable: false),
                    VehicleNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Accommodation = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    AddressInMyanmar = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    TownshipId = table.Column<int>(type: "integer", nullable: false),
                    DistrictId = table.Column<int>(type: "integer", nullable: false),
                    StateRegionId = table.Column<int>(type: "integer", nullable: false),
                    MobileNumberMM = table.Column<string>(type: "varchar(11)", maxLength: 11, nullable: true),
                    PurposeOfVisit = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    PreviousCity = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    HealthDeclaration = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DigitalDeclarations = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    ApprovedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ApprovedUser = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "date", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "date", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArrivalApplication", x => x.AppNo);
                    table.ForeignKey(
                        name: "FK_ArrivalApplication_ModeOfTravel_ModeOfTravelId",
                        column: x => x.ModeOfTravelId,
                        principalTable: "ModeOfTravel",
                        principalColumn: "ModeOfTravelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArrivalApplication_PortOfArrival_PortOfArrivalId",
                        column: x => x.PortOfArrivalId,
                        principalTable: "PortOfArrival",
                        principalColumn: "PortOfArrivalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArrivalApplication_Township_TownshipId",
                        column: x => x.TownshipId,
                        principalTable: "Township",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArrivalApplication_Traveller_TravellerId",
                        column: x => x.TravellerId,
                        principalTable: "Traveller",
                        principalColumn: "TravellerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArrivalApplication_ModeOfTravelId",
                table: "ArrivalApplication",
                column: "ModeOfTravelId");

            migrationBuilder.CreateIndex(
                name: "IX_ArrivalApplication_PortOfArrivalId",
                table: "ArrivalApplication",
                column: "PortOfArrivalId");

            migrationBuilder.CreateIndex(
                name: "IX_ArrivalApplication_TownshipId",
                table: "ArrivalApplication",
                column: "TownshipId");

            migrationBuilder.CreateIndex(
                name: "IX_ArrivalApplication_TravellerId",
                table: "ArrivalApplication",
                column: "TravellerId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_TravellerId",
                table: "AuditLogs",
                column: "TravellerId");

            migrationBuilder.CreateIndex(
                name: "IX_District_SRId",
                table: "District",
                column: "SRId");

            migrationBuilder.CreateIndex(
                name: "IX_NRC_Township_NRC_SRId",
                table: "NRC_Township",
                column: "NRC_SRId");

            migrationBuilder.CreateIndex(
                name: "IX_PortOfArrival_ModeOfTravelId",
                table: "PortOfArrival",
                column: "ModeOfTravelId");

            migrationBuilder.CreateIndex(
                name: "IX_Township_DistrictId",
                table: "Township",
                column: "DistrictId");

            migrationBuilder.CreateIndex(
                name: "IX_Traveller_CountryOfBirthCode",
                table: "Traveller",
                column: "CountryOfBirthCode");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArrivalApplication");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropTable(
                name: "NRC_Township");

            migrationBuilder.DropTable(
                name: "PortOfArrival");

            migrationBuilder.DropTable(
                name: "Township");

            migrationBuilder.DropTable(
                name: "Traveller");

            migrationBuilder.DropTable(
                name: "NRC_StateRegion");

            migrationBuilder.DropTable(
                name: "ModeOfTravel");

            migrationBuilder.DropTable(
                name: "District");

            migrationBuilder.DropTable(
                name: "Country");

            migrationBuilder.DropTable(
                name: "StateRegion");
        }
    }
}
