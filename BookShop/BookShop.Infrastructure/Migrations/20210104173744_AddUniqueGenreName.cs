using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Infrastructure.Migrations
{
    public partial class AddUniqueGenreName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                schema: "BookShop",
                table: "BookShopState",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 100000m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "StorageSize",
                schema: "BookShop",
                table: "BookShopState",
                type: "int",
                nullable: false,
                defaultValue: 100);

            migrationBuilder.CreateIndex(
                name: "IX_Genre_Name",
                schema: "BookShop",
                table: "Genre",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Genre_Name",
                schema: "BookShop",
                table: "Genre");

            migrationBuilder.DropColumn(
                name: "StorageSize",
                schema: "BookShop",
                table: "BookShopState");

            migrationBuilder.AlterColumn<decimal>(
                name: "Balance",
                schema: "BookShop",
                table: "BookShopState",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldDefaultValue: 100000m);
        }
    }
}
