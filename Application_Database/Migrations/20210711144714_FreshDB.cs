using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Application_Database.Migrations
{
    public partial class FreshDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Demo_Customer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Demo_Customer", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Notification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MsgType = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    MsgFrom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MsgTo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MsgCC = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MsgSubject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MsgBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MsgSatus = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    FailDetails = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Notification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tbl_rolemaster",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleNmae = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RecordLocked = table.Column<bool>(type: "bit", nullable: false),
                    RecordLockedBy = table.Column<int>(type: "int", nullable: false),
                    RecordLockedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_rolemaster", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_usermaster",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailId = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RecordLocked = table.Column<bool>(type: "bit", nullable: false),
                    RecordLockedBy = table.Column<int>(type: "int", nullable: false),
                    RecordLockedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_usermaster", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_righmaster",
                columns: table => new
                {
                    RightId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    View = table.Column<bool>(type: "bit", nullable: false),
                    Add = table.Column<bool>(type: "bit", nullable: false),
                    Edit = table.Column<bool>(type: "bit", nullable: false),
                    Delete = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    RecordLocked = table.Column<bool>(type: "bit", nullable: false),
                    RecordLockedBy = table.Column<int>(type: "int", nullable: false),
                    RecordLockedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedBy = table.Column<int>(type: "int", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<int>(type: "int", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_righmaster", x => x.RightId);
                    table.ForeignKey(
                        name: "FK_RoleRights_RoleId",
                        column: x => x.RoleId,
                        principalTable: "tbl_rolemaster",
                        principalColumn: "RoleId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_righmaster_RoleId",
                table: "tbl_righmaster",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Demo_Customer");

            migrationBuilder.DropTable(
                name: "tbl_Notification");

            migrationBuilder.DropTable(
                name: "tbl_righmaster");

            migrationBuilder.DropTable(
                name: "tbl_usermaster");

            migrationBuilder.DropTable(
                name: "tbl_rolemaster");
        }
    }
}