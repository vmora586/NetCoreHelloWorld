using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BooksApi.Migrations
{
    public partial class Add_birth_date : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "birthDate",
                table: "Authors",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "birthDate",
                table: "Authors");
        }
    }
}
