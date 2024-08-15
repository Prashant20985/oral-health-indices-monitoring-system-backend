using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedBeweEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Sectant1",
                schema: "credit",
                table: "PracticeBewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant2",
                schema: "credit",
                table: "PracticeBewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant3",
                schema: "credit",
                table: "PracticeBewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant4",
                schema: "credit",
                table: "PracticeBewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant5",
                schema: "credit",
                table: "PracticeBewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant6",
                schema: "credit",
                table: "PracticeBewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant1",
                schema: "oralHealthExamination",
                table: "Bewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant2",
                schema: "oralHealthExamination",
                table: "Bewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant3",
                schema: "oralHealthExamination",
                table: "Bewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant4",
                schema: "oralHealthExamination",
                table: "Bewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant5",
                schema: "oralHealthExamination",
                table: "Bewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Sectant6",
                schema: "oralHealthExamination",
                table: "Bewe",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sectant1",
                schema: "credit",
                table: "PracticeBewe");

            migrationBuilder.DropColumn(
                name: "Sectant2",
                schema: "credit",
                table: "PracticeBewe");

            migrationBuilder.DropColumn(
                name: "Sectant3",
                schema: "credit",
                table: "PracticeBewe");

            migrationBuilder.DropColumn(
                name: "Sectant4",
                schema: "credit",
                table: "PracticeBewe");

            migrationBuilder.DropColumn(
                name: "Sectant5",
                schema: "credit",
                table: "PracticeBewe");

            migrationBuilder.DropColumn(
                name: "Sectant6",
                schema: "credit",
                table: "PracticeBewe");

            migrationBuilder.DropColumn(
                name: "Sectant1",
                schema: "oralHealthExamination",
                table: "Bewe");

            migrationBuilder.DropColumn(
                name: "Sectant2",
                schema: "oralHealthExamination",
                table: "Bewe");

            migrationBuilder.DropColumn(
                name: "Sectant3",
                schema: "oralHealthExamination",
                table: "Bewe");

            migrationBuilder.DropColumn(
                name: "Sectant4",
                schema: "oralHealthExamination",
                table: "Bewe");

            migrationBuilder.DropColumn(
                name: "Sectant5",
                schema: "oralHealthExamination",
                table: "Bewe");

            migrationBuilder.DropColumn(
                name: "Sectant6",
                schema: "oralHealthExamination",
                table: "Bewe");
        }
    }
}
