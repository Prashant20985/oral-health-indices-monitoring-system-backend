using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedExamEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                schema: "credit",
                table: "Exam");

            migrationBuilder.AddColumn<TimeSpan>(
                name: "DurationInterval",
                schema: "credit",
                table: "Exam",
                type: "interval",
                nullable: false,
                defaultValue: new TimeSpan(0, 0, 0, 0, 0));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DurationInterval",
                schema: "credit",
                table: "Exam");

            migrationBuilder.AddColumn<int>(
                name: "Duration",
                schema: "credit",
                table: "Exam",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
