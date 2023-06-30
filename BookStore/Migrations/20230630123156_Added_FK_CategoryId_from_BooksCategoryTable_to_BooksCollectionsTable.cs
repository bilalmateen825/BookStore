using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStore.Migrations
{
    /// <inheritdoc />
    public partial class Added_FK_CategoryId_from_BooksCategoryTable_to_BooksCollectionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "BooksCollections",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BooksCollections_CategoryID",
                table: "BooksCollections",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_BooksCollections_BooksCategories_CategoryID",
                table: "BooksCollections",
                column: "CategoryID",
                principalTable: "BooksCategories",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BooksCollections_BooksCategories_CategoryID",
                table: "BooksCollections");

            migrationBuilder.DropIndex(
                name: "IX_BooksCollections_CategoryID",
                table: "BooksCollections");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "BooksCollections");
        }
    }
}
