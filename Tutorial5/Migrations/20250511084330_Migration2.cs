using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tutorial5.Migrations
{
    /// <inheritdoc />
    public partial class Migration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Author_AuthorId",
                table: "BookAuthors");

            migrationBuilder.DropForeignKey(
                name: "FK_BookAuthors_Book_BookId",
                table: "BookAuthors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BookAuthors",
                table: "BookAuthors");

            migrationBuilder.RenameTable(
                name: "BookAuthors",
                newName: "Book_Author");

            migrationBuilder.RenameIndex(
                name: "IX_BookAuthors_AuthorId",
                table: "Book_Author",
                newName: "IX_Book_Author_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Book_Author",
                table: "Book_Author",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_Author_AuthorId",
                table: "Book_Author",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Book_Author_Book_BookId",
                table: "Book_Author",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_Author_AuthorId",
                table: "Book_Author");

            migrationBuilder.DropForeignKey(
                name: "FK_Book_Author_Book_BookId",
                table: "Book_Author");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Book_Author",
                table: "Book_Author");

            migrationBuilder.RenameTable(
                name: "Book_Author",
                newName: "BookAuthors");

            migrationBuilder.RenameIndex(
                name: "IX_Book_Author_AuthorId",
                table: "BookAuthors",
                newName: "IX_BookAuthors_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BookAuthors",
                table: "BookAuthors",
                columns: new[] { "BookId", "AuthorId" });

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Author_AuthorId",
                table: "BookAuthors",
                column: "AuthorId",
                principalTable: "Author",
                principalColumn: "AuthorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BookAuthors_Book_BookId",
                table: "BookAuthors",
                column: "BookId",
                principalTable: "Book",
                principalColumn: "BookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
