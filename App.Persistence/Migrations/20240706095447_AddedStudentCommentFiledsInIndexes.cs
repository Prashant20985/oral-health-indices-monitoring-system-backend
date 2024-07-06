using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedStudentCommentFiledsInIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comment",
                schema: "oralHealthExamination",
                table: "DMFT_DMFS",
                newName: "StudentComment");

            migrationBuilder.RenameColumn(
                name: "Comment",
                schema: "oralHealthExamination",
                table: "Bleeding",
                newName: "StudentComment");

            migrationBuilder.RenameColumn(
                name: "Comment",
                schema: "oralHealthExamination",
                table: "Bewe",
                newName: "StudentComment");

            migrationBuilder.RenameColumn(
                name: "Comment",
                schema: "oralHealthExamination",
                table: "API",
                newName: "StudentComment");

            migrationBuilder.AddColumn<string>(
                name: "StudentComment",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorComment",
                schema: "oralHealthExamination",
                table: "DMFT_DMFS",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorComment",
                schema: "oralHealthExamination",
                table: "Bleeding",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorComment",
                schema: "oralHealthExamination",
                table: "Bewe",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DoctorComment",
                schema: "oralHealthExamination",
                table: "API",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentComment",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "DoctorComment",
                schema: "oralHealthExamination",
                table: "DMFT_DMFS");

            migrationBuilder.DropColumn(
                name: "DoctorComment",
                schema: "oralHealthExamination",
                table: "Bleeding");

            migrationBuilder.DropColumn(
                name: "DoctorComment",
                schema: "oralHealthExamination",
                table: "Bewe");

            migrationBuilder.DropColumn(
                name: "DoctorComment",
                schema: "oralHealthExamination",
                table: "API");

            migrationBuilder.RenameColumn(
                name: "StudentComment",
                schema: "oralHealthExamination",
                table: "DMFT_DMFS",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "StudentComment",
                schema: "oralHealthExamination",
                table: "Bleeding",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "StudentComment",
                schema: "oralHealthExamination",
                table: "Bewe",
                newName: "Comment");

            migrationBuilder.RenameColumn(
                name: "StudentComment",
                schema: "oralHealthExamination",
                table: "API",
                newName: "Comment");
        }
    }
}
