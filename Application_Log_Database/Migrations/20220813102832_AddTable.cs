using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application_Log_Database.Migrations
{
    public partial class AddTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APIRequest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scheme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QueryString = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Userid = table.Column<int>(type: "int", nullable: true),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: true)
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
                    Response = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestId = table.Column<int>(type: "int", nullable: true),
                    ResponseDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ReponseStatus = table.Column<bool>(type: "bit", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIResponse", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "APIRequest");

            migrationBuilder.DropTable(
                name: "APIResponse");
        }
    }
}
