using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class AddedDescriptionToCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Categories_CategoryId",
                table: "Issues");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Issues",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Categories",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Categories_CategoryId",
                table: "Issues",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Issues_Categories_CategoryId",
                table: "Issues");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Categories");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "Issues",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Issues_Categories_CategoryId",
                table: "Issues",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
