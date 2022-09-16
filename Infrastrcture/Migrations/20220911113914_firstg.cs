using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastrcture.Migrations
{
    public partial class firstg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "points",
                table: "Users");

            migrationBuilder.AddColumn<string>(
                name: "Cart",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cart",
                table: "Users");

            migrationBuilder.AddColumn<int>(
                name: "points",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
