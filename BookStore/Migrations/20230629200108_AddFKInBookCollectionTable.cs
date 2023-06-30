using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class AddFKInBookCollectionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookCategoryCategoryID",
                table: "BooksCollections",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "BooksCollections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_BooksCollections_BookCategoryCategoryID",
                table: "BooksCollections",
                column: "BookCategoryCategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksCollections_BooksCategories_BookCategoryCategoryID",
                table: "BooksCollections",
                column: "BookCategoryCategoryID",
                principalTable: "BooksCategories",
                principalColumn: "CategoryID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksCollections_BooksCategories_BookCategoryCategoryID",
                table: "BooksCollections");

            migrationBuilder.DropIndex(
                name: "IX_BooksCollections_BookCategoryCategoryID",
                table: "BooksCollections");

            migrationBuilder.DropColumn(
                name: "BookCategoryCategoryID",
                table: "BooksCollections");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "BooksCollections");
        }
    }
}
