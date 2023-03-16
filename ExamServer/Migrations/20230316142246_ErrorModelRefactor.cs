using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExamServer.Migrations
{
    /// <inheritdoc />
    public partial class ErrorModelRefactor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WrongAnswer",
                table: "Errors");

            migrationBuilder.AlterColumn<int>(
                name: "CorrectAnswer",
                table: "Errors",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "SelectedAnswer",
                table: "Errors",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SelectedAnswer",
                table: "Errors");

            migrationBuilder.AlterColumn<string>(
                name: "CorrectAnswer",
                table: "Errors",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "WrongAnswer",
                table: "Errors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
