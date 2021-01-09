using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Infrastructure.Migrations
{
    public partial class AddBookShop : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookShopState",
                schema: "BookShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Balance = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookShopState", x => x.Id);
                });

            migrationBuilder.AddCheckConstraint(
                name: "CK_BookShop.Book_Cost",
                schema: "BookShop",
                table: "Book",
                sql: "[Cost] > 0");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookShopState",
                schema: "BookShop");

            migrationBuilder.DropCheckConstraint(
                name: "CK_BookShop.Book_Cost",
                schema: "BookShop",
                table: "Book");
        }
    }
}
