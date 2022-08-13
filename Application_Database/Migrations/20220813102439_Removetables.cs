using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application_Database.Migrations
{
    public partial class Removetables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIRequest");

            migrationBuilder.DropTable(
                name: "APIResponse");

            migrationBuilder.AddColumn<int>(
                name: "AlloweNoBranch",
                table: "AdminOrganization",
                type: "int",
                nullable: true,
                defaultValueSql: "((2))");

            migrationBuilder.AddColumn<int>(
                name: "AlloweNoComp",
                table: "AdminOrganization",
                type: "int",
                nullable: true,
                defaultValueSql: "((2))");

            migrationBuilder.AddColumn<int>(
                name: "AlloweNoUser",
                table: "AdminOrganization",
                type: "int",
                nullable: true,
                defaultValueSql: "((5))");

            migrationBuilder.AddColumn<bool>(
                name: "IsCompProductWise",
                table: "AdminOrganization",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))");

            migrationBuilder.AddColumn<bool>(
                name: "IsMasterCompWise",
                table: "AdminOrganization",
                type: "bit",
                nullable: true,
                defaultValueSql: "((1))");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AlloweNoBranch",
                table: "AdminOrganization");

            migrationBuilder.DropColumn(
                name: "AlloweNoComp",
                table: "AdminOrganization");

            migrationBuilder.DropColumn(
                name: "AlloweNoUser",
                table: "AdminOrganization");

            migrationBuilder.DropColumn(
                name: "IsCompProductWise",
                table: "AdminOrganization");

            migrationBuilder.DropColumn(
                name: "IsMasterCompWise",
                table: "AdminOrganization");

            migrationBuilder.CreateTable(
                name: "APIRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Path = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QueryString = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Scheme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Userid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIRequest", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "APIResponse",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReponseStatus = table.Column<bool>(type: "bit", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIResponse", x => x.Id);
                });
        }
    }
}
