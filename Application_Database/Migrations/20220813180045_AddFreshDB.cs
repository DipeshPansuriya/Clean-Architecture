using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Application_Database.Migrations
{
    public partial class AddFreshDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AdminMenu",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    MenuName = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ParentMenuId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsSysAdmin = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminMenu", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "AdminOrganization",
                columns: table => new
                {
                    OrgId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    OrgEmail = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    AlloweNoComp = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((2))"),
                    AlloweNoBranch = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((2))"),
                    AlloweNoUser = table.Column<int>(type: "int", nullable: true, defaultValueSql: "((5))"),
                    IsCompProductWise = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsMasterCompWise = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
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
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminProduct", x => x.ProductId);
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
                    ProdId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
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

            migrationBuilder.CreateTable(
                name: "AdminCompany",
                columns: table => new
                {
                    CompId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgProdId = table.Column<int>(type: "int", nullable: false),
                    CompName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address3 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    TelephoneNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminCompany", x => x.CompId);
                    table.ForeignKey(
                        name: "FK_AdminCompany_AdminOrgProduct",
                        column: x => x.OrgProdId,
                        principalTable: "AdminOrgProduct",
                        principalColumn: "OrgProdId");
                });

            migrationBuilder.CreateTable(
                name: "AdminRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgProdId = table.Column<int>(type: "int", nullable: true),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminRole", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_AdminRole_AdminOrgProduct",
                        column: x => x.OrgProdId,
                        principalTable: "AdminOrgProduct",
                        principalColumn: "OrgProdId");
                });

            migrationBuilder.CreateTable(
                name: "AdminUser",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgProdId = table.Column<int>(type: "int", nullable: false),
                    LoginMail = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    LoginPassword = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUser", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AdminUser_AdminOrgProduct",
                        column: x => x.OrgProdId,
                        principalTable: "AdminOrgProduct",
                        principalColumn: "OrgProdId");
                });

            migrationBuilder.CreateTable(
                name: "AdminBranch",
                columns: table => new
                {
                    BranchId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompId = table.Column<int>(type: "int", nullable: false),
                    OrgProdId = table.Column<int>(type: "int", nullable: false),
                    BranchName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Address1 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address2 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Address3 = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsHo = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    IsActive = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((1))"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))"),
                    CreatedBy = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    DeletedBy = table.Column<int>(type: "int", nullable: true),
                    DeletedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminBranch", x => x.BranchId);
                    table.ForeignKey(
                        name: "FK_AdminBranch_AdminCompany",
                        column: x => x.CompId,
                        principalTable: "AdminCompany",
                        principalColumn: "CompId");
                    table.ForeignKey(
                        name: "FK_AdminBranch_AdminOrgProduct",
                        column: x => x.OrgProdId,
                        principalTable: "AdminOrgProduct",
                        principalColumn: "OrgProdId");
                });

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

            migrationBuilder.CreateTable(
                name: "AdminUserBranch",
                columns: table => new
                {
                    Userbranchid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrgProdId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: true, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdminUserBranch", x => x.Userbranchid);
                    table.ForeignKey(
                        name: "FK_AdminUserBranch_AdminBranch",
                        column: x => x.BranchId,
                        principalTable: "AdminBranch",
                        principalColumn: "BranchId");
                    table.ForeignKey(
                        name: "FK_AdminUserBranch_AdminCompany",
                        column: x => x.CompanyId,
                        principalTable: "AdminCompany",
                        principalColumn: "CompId");
                    table.ForeignKey(
                        name: "FK_AdminUserBranch_AdminOrgProduct",
                        column: x => x.OrgProdId,
                        principalTable: "AdminOrgProduct",
                        principalColumn: "OrgProdId");
                    table.ForeignKey(
                        name: "FK_AdminUserBranch_AdminRole",
                        column: x => x.RoleId,
                        principalTable: "AdminRole",
                        principalColumn: "RoleId");
                    table.ForeignKey(
                        name: "FK_AdminUserBranch_AdminUser",
                        column: x => x.UserId,
                        principalTable: "AdminUser",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdminBranch_CompId",
                table: "AdminBranch",
                column: "CompId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminBranch_OrgProdId",
                table: "AdminBranch",
                column: "OrgProdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminCompany_OrgProdId",
                table: "AdminCompany",
                column: "OrgProdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminOrgProduct_Orgid",
                table: "AdminOrgProduct",
                column: "Orgid");

            migrationBuilder.CreateIndex(
                name: "IX_AdminOrgProduct_ProdId",
                table: "AdminOrgProduct",
                column: "ProdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRights_MenuId",
                table: "AdminRights",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRights_RoleId",
                table: "AdminRights",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminRole_OrgProdId",
                table: "AdminRole",
                column: "OrgProdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUser_OrgProdId",
                table: "AdminUser",
                column: "OrgProdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserBranch_BranchId",
                table: "AdminUserBranch",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserBranch_CompanyId",
                table: "AdminUserBranch",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserBranch_OrgProdId",
                table: "AdminUserBranch",
                column: "OrgProdId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserBranch_RoleId",
                table: "AdminUserBranch",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_AdminUserBranch_UserId",
                table: "AdminUserBranch",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdminRights");

            migrationBuilder.DropTable(
                name: "AdminUserBranch");

            migrationBuilder.DropTable(
                name: "StatusNotification");

            migrationBuilder.DropTable(
                name: "AdminMenu");

            migrationBuilder.DropTable(
                name: "AdminBranch");

            migrationBuilder.DropTable(
                name: "AdminRole");

            migrationBuilder.DropTable(
                name: "AdminUser");

            migrationBuilder.DropTable(
                name: "AdminCompany");

            migrationBuilder.DropTable(
                name: "AdminOrgProduct");

            migrationBuilder.DropTable(
                name: "AdminOrganization");

            migrationBuilder.DropTable(
                name: "AdminProduct");
        }
    }
}
