using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Infrastructure.Migrations
{
    public partial class AddRequiredGenreName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Genre_Name",
                schema: "BookShop",
                table: "Genre");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BookShop",
                table: "Genre",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Name",
                schema: "BookShop",
                table: "Genre",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Genre_Name",
                schema: "BookShop",
                table: "Genre");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "BookShop",
                table: "Genre",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Name",
                schema: "BookShop",
                table: "Genre",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}
