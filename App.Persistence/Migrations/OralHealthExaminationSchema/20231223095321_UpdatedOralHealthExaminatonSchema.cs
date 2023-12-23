using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations.OralHealthExaminationSchema
{
    /// <inheritdoc />
    public partial class UpdatedOralHealthExaminatonSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_ResearchGroup_PatientGroupId",
                schema: "oralHealthExamination",
                table: "Patient");

            migrationBuilder.RenameColumn(
                name: "PatientGroupId",
                schema: "oralHealthExamination",
                table: "Patient",
                newName: "ResearchGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_PatientGroupId",
                schema: "oralHealthExamination",
                table: "Patient",
                newName: "IX_Patient_ResearchGroupId");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                schema: "oralHealthExamination",
                table: "ResearchGroup",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                schema: "oralHealthExamination",
                table: "ResearchGroup",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_ResearchGroup_ResearchGroupId",
                schema: "oralHealthExamination",
                table: "Patient",
                column: "ResearchGroupId",
                principalSchema: "oralHealthExamination",
                principalTable: "ResearchGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Patient_ResearchGroup_ResearchGroupId",
                schema: "oralHealthExamination",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                schema: "oralHealthExamination",
                table: "ResearchGroup");

            migrationBuilder.DropColumn(
                name: "Description",
                schema: "oralHealthExamination",
                table: "ResearchGroup");

            migrationBuilder.RenameColumn(
                name: "ResearchGroupId",
                schema: "oralHealthExamination",
                table: "Patient",
                newName: "PatientGroupId");

            migrationBuilder.RenameIndex(
                name: "IX_Patient_ResearchGroupId",
                schema: "oralHealthExamination",
                table: "Patient",
                newName: "IX_Patient_PatientGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Patient_ResearchGroup_PatientGroupId",
                schema: "oralHealthExamination",
                table: "Patient",
                column: "PatientGroupId",
                principalSchema: "oralHealthExamination",
                principalTable: "ResearchGroup",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
