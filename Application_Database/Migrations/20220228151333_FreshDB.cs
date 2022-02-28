using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application_Database.Migrations
{
    public partial class FreshDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminOrganization",
                columns: table => new
                {
                    OrgId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrgEmail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminOrganization", x => x.OrgId);
                });

            migrationBuilder.CreateTable(
                name: "AdminProduct",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminProduct", x => x.ProductId);
                });

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

            migrationBuilder.CreateTable(
                name: "StatusNotification",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MsgType = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    MsgFrom = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    MsgTo = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MsgCC = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    MsgSubject = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    MsgBody = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MsgSatus = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FailDetails = table.Column<string>(type: "nvarchar(2000)", maxLength: 2000, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    UpdatedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatusNotification", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AdminOrgProduct",
                columns: table => new
                {
                    OrgProdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Orgid = table.Column<int>(type: "int", nullable: false),
                    ProdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminOrgProduct", x => x.OrgProdId);
                    table.ForeignKey(
                        name: "FK_AdminOrgProduct_AdminOrganization",
                        column: x => x.Orgid,
                        principalTable: "AdminOrganization",
                        principalColumn: "OrgId");
                    table.ForeignKey(
                        name: "FK_AdminOrgProduct_AdminProduct",
                        column: x => x.ProdId,
                        principalTable: "AdminProduct",
                        principalColumn: "ProductId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminOrgProduct_Orgid",
                table: "AdminOrgProduct",
                column: "Orgid");

            migrationBuilder.CreateIndex(
                name: "IX_AdminOrgProduct_ProdId",
                table: "AdminOrgProduct",
                column: "ProdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminOrgProduct");

            migrationBuilder.DropTable(
                name: "APIRequest");

            migrationBuilder.DropTable(
                name: "APIResponse");

            migrationBuilder.DropTable(
                name: "StatusNotification");

            migrationBuilder.DropTable(
                name: "AdminOrganization");

            migrationBuilder.DropTable(
                name: "AdminProduct");
        }
    }
}
