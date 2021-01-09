using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookShop.Infrastructure.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "BookShop");

            migrationBuilder.CreateTable(
                name: "BookInfo",
                schema: "BookShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genre",
                schema: "BookShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genre", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Book",
                schema: "BookShop",
                columns: table => new
                {
                    Guid = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ArriveDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookInfoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_Book_BookInfo_BookInfoId",
                        column: x => x.BookInfoId,
                        principalSchema: "BookShop",
                        principalTable: "BookInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookInfoGenre",
                schema: "BookShop",
                columns: table => new
                {
                    BookInfosId = table.Column<int>(type: "int", nullable: false),
                    GenresId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookInfoGenre", x => new { x.BookInfosId, x.GenresId });
                    table.ForeignKey(
                        name: "FK_BookInfoGenre_BookInfo_BookInfosId",
                        column: x => x.BookInfosId,
                        principalSchema: "BookShop",
                        principalTable: "BookInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookInfoGenre_Genre_GenresId",
                        column: x => x.GenresId,
                        principalSchema: "BookShop",
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Discount",
                schema: "BookShop",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExpireDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookInfoId = table.Column<int>(type: "int", nullable: true),
                    BookGuid = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GenreId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discount", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Discount_Book_BookGuid",
                        column: x => x.BookGuid,
                        principalSchema: "BookShop",
                        principalTable: "Book",
                        principalColumn: "Guid",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Discount_BookInfo_BookInfoId",
                        column: x => x.BookInfoId,
                        principalSchema: "BookShop",
                        principalTable: "BookInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Discount_Genre_GenreId",
                        column: x => x.GenreId,
                        principalSchema: "BookShop",
                        principalTable: "Genre",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_BookInfoId",
                schema: "BookShop",
                table: "Book",
                column: "BookInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_BookInfoGenre_GenresId",
                schema: "BookShop",
                table: "BookInfoGenre",
                column: "GenresId");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_BookGuid",
                schema: "BookShop",
                table: "Discount",
                column: "BookGuid");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_BookInfoId",
                schema: "BookShop",
                table: "Discount",
                column: "BookInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Discount_GenreId",
                schema: "BookShop",
                table: "Discount",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookInfoGenre",
                schema: "BookShop");

            migrationBuilder.DropTable(
                name: "Discount",
                schema: "BookShop");

            migrationBuilder.DropTable(
                name: "Book",
                schema: "BookShop");

            migrationBuilder.DropTable(
                name: "Genre",
                schema: "BookShop");

            migrationBuilder.DropTable(
                name: "BookInfo",
                schema: "BookShop");
        }
    }
}
