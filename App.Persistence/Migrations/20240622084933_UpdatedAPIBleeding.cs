using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedAPIBleeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Mandible",
                schema: "credit",
                table: "PracticeBleeding",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Maxilla",
                schema: "credit",
                table: "PracticeBleeding",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mandible",
                schema: "credit",
                table: "PracticeAPI",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Maxilla",
                schema: "credit",
                table: "PracticeAPI",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mandible",
                schema: "oralHealthExamination",
                table: "Bleeding",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Maxilla",
                schema: "oralHealthExamination",
                table: "Bleeding",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Mandible",
                schema: "oralHealthExamination",
                table: "API",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Maxilla",
                schema: "oralHealthExamination",
                table: "API",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mandible",
                schema: "credit",
                table: "PracticeBleeding");

            migrationBuilder.DropColumn(
                name: "Maxilla",
                schema: "credit",
                table: "PracticeBleeding");

            migrationBuilder.DropColumn(
                name: "Mandible",
                schema: "credit",
                table: "PracticeAPI");

            migrationBuilder.DropColumn(
                name: "Maxilla",
                schema: "credit",
                table: "PracticeAPI");

            migrationBuilder.DropColumn(
                name: "Mandible",
                schema: "oralHealthExamination",
                table: "Bleeding");

            migrationBuilder.DropColumn(
                name: "Maxilla",
                schema: "oralHealthExamination",
                table: "Bleeding");

            migrationBuilder.DropColumn(
                name: "Mandible",
                schema: "oralHealthExamination",
                table: "API");

            migrationBuilder.DropColumn(
                name: "Maxilla",
                schema: "oralHealthExamination",
                table: "API");
        }
    }
}
