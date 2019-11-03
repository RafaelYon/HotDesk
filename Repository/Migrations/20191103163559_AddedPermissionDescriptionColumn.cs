using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddedPermissionDescriptionColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Permissions",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "CreatedAt", "Description", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2019, 11, 3, 14, 35, 58, 532, DateTimeKind.Local), "Criar chamado", "IssueCreate", new DateTime(2019, 11, 3, 14, 35, 58, 538, DateTimeKind.Local) },
                    { 2, new DateTime(2019, 11, 3, 14, 35, 58, 539, DateTimeKind.Local), "Assumir chamado", "IssueAccept", new DateTime(2019, 11, 3, 14, 35, 58, 539, DateTimeKind.Local) },
                    { 3, new DateTime(2019, 11, 3, 14, 35, 58, 539, DateTimeKind.Local), "Encerrar chamado", "IssueClose", new DateTime(2019, 11, 3, 14, 35, 58, 539, DateTimeKind.Local) },
                    { 4, new DateTime(2019, 11, 3, 14, 35, 58, 539, DateTimeKind.Local), "Escalar chamado", "IssueEscalate", new DateTime(2019, 11, 3, 14, 35, 58, 539, DateTimeKind.Local) },
                    { 5, new DateTime(2019, 11, 3, 14, 35, 58, 539, DateTimeKind.Local), "Avaliar assitencia", "IssueRateAssistence", new DateTime(2019, 11, 3, 14, 35, 58, 539, DateTimeKind.Local) },
                    { 6, new DateTime(2019, 11, 3, 14, 35, 58, 540, DateTimeKind.Local), "Gerenciar contas", "ManageAccounts", new DateTime(2019, 11, 3, 14, 35, 58, 540, DateTimeKind.Local) },
                    { 7, new DateTime(2019, 11, 3, 14, 35, 58, 540, DateTimeKind.Local), "Gerenciar grupos", "ManageGroups", new DateTime(2019, 11, 3, 14, 35, 58, 540, DateTimeKind.Local) }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Permissions",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Permissions");
        }
    }
}
