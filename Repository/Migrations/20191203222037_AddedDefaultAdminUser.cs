using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddedDefaultAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Image", "Name", "Password", "UpdatedAt" },
                values: new object[] { 1, new DateTime(2019, 12, 3, 14, 34, 0, 0, DateTimeKind.Unspecified), "admin@hotdesk.com", "iVBORw0KGgoAAAANSUhEUgAAAEEAAABBAQMAAAC0OVsGAAAABlBMVEX///84SyvAiwjYAAAAAnRSTlMA/1uRIrUAAAAiSURBVHicY2Bg/w8EHxiAYOSyBhz8h4KGgWP9ALmDf+BYACldHJem9JdHAAAAAElFTkSuQmCC", "Admin", "$2a$11$R0eCbYJa.keZBbcvJxtmu.pNFH7xS/q3e9izFAYae8dQAgb4ky2Z6", new DateTime(2019, 12, 3, 14, 24, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "GroupUser",
                columns: new[] { "GroupId", "UserId" },
                values: new object[] { 3, 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "GroupUser",
                keyColumns: new[] { "GroupId", "UserId" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
