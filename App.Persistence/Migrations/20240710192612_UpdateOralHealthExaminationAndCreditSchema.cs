using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOralHealthExaminationAndCreditSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "credit",
                table: "PracticePatientExaminationCard",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NeedForDentalInterventions",
                schema: "credit",
                table: "PracticePatientExaminationCard",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientRecommendations",
                schema: "credit",
                table: "PracticePatientExaminationCard",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProposedTreatment",
                schema: "credit",
                table: "PracticePatientExaminationCard",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProstheticStatus",
                schema: "credit",
                table: "PracticeDMFT_DMFS",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NeedForDentalInterventions",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PatientRecommendations",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProposedTreatment",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProstheticStatus",
                schema: "oralHealthExamination",
                table: "DMFT_DMFS",
                type: "character varying(500)",
                maxLength: 500,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                schema: "credit",
                table: "PracticePatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "NeedForDentalInterventions",
                schema: "credit",
                table: "PracticePatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "PatientRecommendations",
                schema: "credit",
                table: "PracticePatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "ProposedTreatment",
                schema: "credit",
                table: "PracticePatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "ProstheticStatus",
                schema: "credit",
                table: "PracticeDMFT_DMFS");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "NeedForDentalInterventions",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "PatientRecommendations",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "ProposedTreatment",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "ProstheticStatus",
                schema: "oralHealthExamination",
                table: "DMFT_DMFS");
        }
    }
}
