using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Library.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    ListPrice30 = table.Column<double>(type: "float", nullable: false),
                    ListPriceHigher30 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "ListPrice30", "ListPriceHigher30", "Title" },
                values: new object[,]
                {
                    { 1, "J.K Rowling", "The hope and wonder of Harry Potter's world will make you want to escape to Hogwarts again and again. The magic starts here!", "1408855658", 11.9, 7.5300000000000002, 5.5499999999999998, "Harry Potter And The Philosopher'S Stone" },
                    { 2, "J.R.R Tolkien", "It is the ancient drama to which the characters in The Lord of the Rings look back, and in whose events some of them such as Elrond and Galadriel took part.", "9780261102736", 8.0, 6.2300000000000004, 4.6500000000000004, "The Silmarillion" },
                    { 3, "Isaac Asimov", "In these stories Isaac Asimov creates the Three Laws of Robotics and ushers in the Robot Age.\r\n\r\nEarth is ruled by master-machines but the Three Laws of Robotics have been designed to ensure humans maintain the upper hand.", "9780007532278", 10.0, 7.4299999999999997, 5.25, "I, Robot" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30);
        }
    }
}
