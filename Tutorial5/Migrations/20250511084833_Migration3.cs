using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Tutorial5.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Author",
                columns: new[] { "AuthorId", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "Jane", "Doe" },
                    { 2, "John", "Doe" }
                });

            migrationBuilder.InsertData(
                table: "Book",
                columns: new[] { "BookId", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Book 1", 10.199999999999999 },
                    { 2, "Book 2", 123.2 }
                });

            migrationBuilder.InsertData(
                table: "Book_Author",
                columns: new[] { "AuthorId", "BookId", "Notes" },
                values: new object[,]
                {
                    { 1, 1, "n1" },
                    { 2, 1, "n2" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book_Author",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Book_Author",
                keyColumns: new[] { "AuthorId", "BookId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Author",
                keyColumn: "AuthorId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Book",
                keyColumn: "BookId",
                keyValue: 1);
        }
    }
}
