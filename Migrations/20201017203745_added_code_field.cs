using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksApi.Migrations
{
    public partial class added_code_field : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "code",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "code",
                table: "Authors");
        }
    }
}
