using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastrcture.Migrations
{
    public partial class iii : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MainQuestions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionNumber = table.Column<int>(type: "int", nullable: false),
                    CorrectAnswer = table.Column<int>(type: "int", nullable: false),
                    Questinon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer3 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Answer4 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MainQuestions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MainQuestions");
        }
    }
}
