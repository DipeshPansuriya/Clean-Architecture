using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Users_Database.Migrations
{
    public partial class MenuMasterChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MenuURL",
                table: "AdminMenu",
                type: "nvarchar(250)",
                maxLength: 250,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuURL",
                table: "AdminMenu");
        }
    }
}