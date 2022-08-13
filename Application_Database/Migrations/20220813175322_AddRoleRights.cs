using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application_Database.Migrations
{
    public partial class AddRoleRights : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminRights",
                columns: table => new
                {
                    RightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    ViewAccess = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    AddAccess = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    EditAccess = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    DeleteAccess = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRights", x => x.RightId);
                    table.ForeignKey(
                        name: "FK_AdminRights_AdminMenu",
                        column: x => x.MenuId,
                        principalTable: "AdminMenu",
                        principalColumn: "MenuId");
                    table.ForeignKey(
                        name: "FK_AdminRights_AdminRole",
                        column: x => x.RoleId,
                        principalTable: "AdminRole",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminRights_MenuId",
                table: "AdminRights",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRights_RoleId",
                table: "AdminRights",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminRights");
        }
    }
}
