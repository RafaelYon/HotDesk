using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class SimplifiedPermissionStruct : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GroupPermission_Permissions_PermissionId",
                table: "GroupPermission");

            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermission",
                table: "GroupPermission");

            migrationBuilder.DropIndex(
                name: "IX_GroupPermission_PermissionId",
                table: "GroupPermission");

            migrationBuilder.DropColumn(
                name: "PermissionId",
                table: "GroupPermission");

            migrationBuilder.AddColumn<int>(
                name: "Permission",
                table: "GroupPermission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermission",
                table: "GroupPermission",
                columns: new[] { "GroupId", "Permission" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GroupPermission",
                table: "GroupPermission");

            migrationBuilder.DropColumn(
                name: "Permission",
                table: "GroupPermission");

            migrationBuilder.AddColumn<int>(
                name: "PermissionId",
                table: "GroupPermission",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GroupPermission",
                table: "GroupPermission",
                columns: new[] { "GroupId", "PermissionId" });

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    UpdatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 11, 12, 20, 46, 23, 144, DateTimeKind.Local), "Criar chamado", "IssueCreate", new DateTime(2019, 11, 12, 20, 46, 23, 147, DateTimeKind.Local) },
                    { 2, new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local), "Assumir chamado", "IssueAccept", new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local) },
                    { 3, new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local), "Encerrar chamado", "IssueClose", new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local) },
                    { 4, new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local), "Escalar chamado", "IssueEscalate", new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local) },
                    { 5, new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local), "Avaliar assitencia", "IssueRateAssistence", new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local) },
                    { 6, new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local), "Gerenciar contas", "ManageAccounts", new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local) },
                    { 7, new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local), "Gerenciar grupos", "ManageGroups", new DateTime(2019, 11, 12, 20, 46, 23, 148, DateTimeKind.Local) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupPermission_PermissionId",
                table: "GroupPermission",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_Permissions_Name",
                table: "Permissions",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_GroupPermission_Permissions_PermissionId",
                table: "GroupPermission",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
