using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application_Database.Migrations
{
    public partial class tblChangesResponseRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "apidate",
                table: "tbl_API_Response");

            migrationBuilder.DropColumn(
                name: "filename",
                table: "tbl_API_Response");

            migrationBuilder.DropColumn(
                name: "request",
                table: "tbl_API_Response");

            migrationBuilder.RenameColumn(
                name: "userid",
                table: "tbl_API_Response",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "response",
                table: "tbl_API_Response",
                newName: "Response");

            migrationBuilder.RenameColumn(
                name: "apistauts",
                table: "tbl_API_Response",
                newName: "ReponseStatus");

            migrationBuilder.AlterDatabase(
                oldCollation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "tbl_Notification",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RequestId",
                table: "tbl_API_Response",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ResponseDate",
                table: "tbl_API_Response",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tbl_API_Request",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Scheme = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Path = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    QueryString = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Userid = table.Column<int>(type: "int", nullable: true),
                    Request = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_API_Request", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_API_Request");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "tbl_Notification");

            migrationBuilder.DropColumn(
                name: "RequestId",
                table: "tbl_API_Response");

            migrationBuilder.DropColumn(
                name: "ResponseDate",
                table: "tbl_API_Response");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "tbl_API_Response",
                newName: "userid");

            migrationBuilder.RenameColumn(
                name: "Response",
                table: "tbl_API_Response",
                newName: "response");

            migrationBuilder.RenameColumn(
                name: "ReponseStatus",
                table: "tbl_API_Response",
                newName: "apistauts");

            migrationBuilder.AlterDatabase(
                collation: "SQL_Latin1_General_CP1_CI_AS");

            migrationBuilder.AddColumn<DateTime>(
                name: "apidate",
                table: "tbl_API_Response",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())");

            migrationBuilder.AddColumn<string>(
                name: "filename",
                table: "tbl_API_Response",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "request",
                table: "tbl_API_Response",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
