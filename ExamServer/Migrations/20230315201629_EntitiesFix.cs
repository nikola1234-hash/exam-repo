using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Server.Migrations
{
    /// <inheritdoc />
    public partial class EntitiesFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Errors_ExamResults_ExamResultId",
                table: "Errors");

            migrationBuilder.AlterColumn<int>(
                name: "ExamResultId",
                table: "Errors",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Errors_ExamResults_ExamResultId",
                table: "Errors",
                column: "ExamResultId",
                principalTable: "ExamResults",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Errors_ExamResults_ExamResultId",
                table: "Errors");

            migrationBuilder.AlterColumn<int>(
                name: "ExamResultId",
                table: "Errors",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Errors_ExamResults_ExamResultId",
                table: "Errors",
                column: "ExamResultId",
                principalTable: "ExamResults",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
