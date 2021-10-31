using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Application_Database.Migrations
{
    public partial class AlterTable_Notificaion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "tbl_Notification",
                type: "datetime",
                nullable: true,
                defaultValueSql: "(getdate())");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "tbl_Notification");
        }
    }
}
