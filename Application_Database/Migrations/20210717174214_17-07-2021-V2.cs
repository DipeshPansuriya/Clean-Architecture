using Microsoft.EntityFrameworkCore.Migrations;

namespace Application_Database.Migrations
{
    public partial class _17072021V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_righmaster",
                table: "tbl_righmaster");

            migrationBuilder.RenameTable(
                name: "tbl_righmaster",
                newName: "tbl_rightmaster");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_righmaster_RoleId",
                table: "tbl_rightmaster",
                newName: "IX_tbl_rightmaster_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_rightmaster",
                table: "tbl_rightmaster",
                column: "RightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tbl_rightmaster",
                table: "tbl_rightmaster");

            migrationBuilder.RenameTable(
                name: "tbl_rightmaster",
                newName: "tbl_righmaster");

            migrationBuilder.RenameIndex(
                name: "IX_tbl_rightmaster_RoleId",
                table: "tbl_righmaster",
                newName: "IX_tbl_righmaster_RoleId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tbl_righmaster",
                table: "tbl_righmaster",
                column: "RightId");
        }
    }
}
