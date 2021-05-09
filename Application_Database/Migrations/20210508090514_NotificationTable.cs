using Microsoft.EntityFrameworkCore.Migrations;

namespace Application_Database.Migrations
{
    public partial class NotificationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tbl_Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MsgType = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    MsgFrom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    MsgTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MsgCC = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MsgSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MsgBody = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Notification", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tbl_Notification");
        }
    }
}