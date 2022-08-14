using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Masters_Database.Migrations
{
    public partial class FreshMasterDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MasterCurrency",
                columns: table => new
                {
                    CurrencyId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(3)", maxLength: 3, nullable: false),
                    Symbole = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSystemGenrated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCurrency", x => x.CurrencyId);
                });

            migrationBuilder.CreateTable(
                name: "MastersContinent",
                columns: table => new
                {
                    ContinentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSystemGenrated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MastersContinent", x => x.ContinentId);
                });

            migrationBuilder.CreateTable(
                name: "MasterTimeZone",
                columns: table => new
                {
                    TimeZoneId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OffSet = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    OffSetName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSystemGenrated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterTimeZone", x => x.TimeZoneId);
                });

            migrationBuilder.CreateTable(
                name: "MasterCountry",
                columns: table => new
                {
                    CountryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CodeTwoLtr = table.Column<string>(type: "nvarchar(4)", maxLength: 4, nullable: false),
                    CodeThreeLtr = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    NumericCode = table.Column<int>(type: "int", nullable: true),
                    ContinentId = table.Column<int>(type: "int", nullable: false),
                    TimeZoneId = table.Column<int>(type: "int", nullable: true),
                    CurrencyId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSystemGenrated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCountry", x => x.CountryId);
                    table.ForeignKey(
                        name: "FK_MasterCountry_MasterCurrency",
                        column: x => x.CurrencyId,
                        principalTable: "MasterCurrency",
                        principalColumn: "CurrencyId");
                    table.ForeignKey(
                        name: "FK_MasterCountry_MastersContinent",
                        column: x => x.ContinentId,
                        principalTable: "MastersContinent",
                        principalColumn: "ContinentId");
                    table.ForeignKey(
                        name: "FK_MasterCountry_MasterTimeZone",
                        column: x => x.TimeZoneId,
                        principalTable: "MasterTimeZone",
                        principalColumn: "TimeZoneId");
                });

            migrationBuilder.CreateTable(
                name: "MasterState",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    NumericCode = table.Column<int>(type: "int", nullable: true),
                    CountryId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSystemGenrated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterState", x => x.StateId);
                    table.ForeignKey(
                        name: "FK_MasterState_MasterCountry",
                        column: x => x.CountryId,
                        principalTable: "MasterCountry",
                        principalColumn: "CountryId");
                });

            migrationBuilder.CreateTable(
                name: "MasterCity",
                columns: table => new
                {
                    CityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(6)", maxLength: 6, nullable: false),
                    NumericCode = table.Column<int>(type: "int", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsSystemGenrated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MasterCity", x => x.CityId);
                    table.ForeignKey(
                        name: "FK_MasterCity_MasterState",
                        column: x => x.StateId,
                        principalTable: "MasterState",
                        principalColumn: "StateId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MasterCity_StateId",
                table: "MasterCity",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCountry_ContinentId",
                table: "MasterCountry",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCountry_CurrencyId",
                table: "MasterCountry",
                column: "CurrencyId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterCountry_TimeZoneId",
                table: "MasterCountry",
                column: "TimeZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_MasterState_CountryId",
                table: "MasterState",
                column: "CountryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MasterCity");

            migrationBuilder.DropTable(
                name: "MasterState");

            migrationBuilder.DropTable(
                name: "MasterCountry");

            migrationBuilder.DropTable(
                name: "MasterCurrency");

            migrationBuilder.DropTable(
                name: "MastersContinent");

            migrationBuilder.DropTable(
                name: "MasterTimeZone");
        }
    }
}
