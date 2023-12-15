using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations.OralHealthExaminationSchema
{
    /// <inheritdoc />
    public partial class Updated_UpdatedOralHealthExaminationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PatientExaminationResultId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_PatientExaminationResultId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationResultId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientExaminationCard_PatientExaminationResult_PatientExaminationResultId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationResultId",
                principalSchema: "oralHealthExamination",
                principalTable: "PatientExaminationResult",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientExaminationCard_PatientExaminationResult_PatientExaminationResultId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropIndex(
                name: "IX_PatientExaminationCard_PatientExaminationResultId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "PatientExaminationResultId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");
        }
    }
}
