using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamServer.Migrations
{
    /// <inheritdoc />
    public partial class QuestionFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CorrectAnswerIndex",
                table: "Questions",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CorrectAnswerIndex",
                table: "Questions");
        }
    }
}
