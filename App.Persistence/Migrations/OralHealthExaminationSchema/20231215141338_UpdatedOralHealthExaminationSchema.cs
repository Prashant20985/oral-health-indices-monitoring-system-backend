using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations.OralHealthExaminationSchema
{
    /// <inheritdoc />
    public partial class UpdatedOralHealthExaminationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "APIBleeding",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    APIResult = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    BleedingResult = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_APIBleeding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bewe",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeweResult = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bewe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DMFT_DMFS",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DMFTResult = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DMFSResult = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMFT_DMFS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientExaminationRegularMode",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateOfExamination = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientExaminationRegularMode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientExaminationRegularMode_ApplicationUser_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PatientExaminationTestMode",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StudentId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DateOfExamination = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentMarks = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientExaminationTestMode", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientExaminationTestMode_ApplicationUser_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PatientExaminationTestMode_ApplicationUser_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RiskFactorAssessment",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiskFactorAssessmentModel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskFactorAssessment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PatientExaminationResult",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BeweId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DMFT_DMFSId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    APIBleedingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientExaminationResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientExaminationResult_APIBleeding_APIBleedingId",
                        column: x => x.APIBleedingId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "APIBleeding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientExaminationResult_Bewe_BeweId",
                        column: x => x.BeweId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "Bewe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientExaminationResult_DMFT_DMFS_DMFT_DMFSId",
                        column: x => x.DMFT_DMFSId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "DMFT_DMFS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientExaminationCard",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DoctorComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    PatientId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RiskFactorAssesmentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PatientExaminationRegularModeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PatientExaminationTestModeId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientExaminationCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientExaminationCard_PatientExaminationRegularMode_PatientExaminationRegularModeId",
                        column: x => x.PatientExaminationRegularModeId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "PatientExaminationRegularMode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientExaminationCard_PatientExaminationTestMode_PatientExaminationTestModeId",
                        column: x => x.PatientExaminationTestModeId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "PatientExaminationTestMode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientExaminationCard_Patient_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientExaminationCard_RiskFactorAssessment_RiskFactorAssesmentId",
                        column: x => x.RiskFactorAssesmentId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "RiskFactorAssessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_PatientExaminationRegularModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationRegularModeId",
                unique: true,
                filter: "[PatientExaminationRegularModeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_PatientExaminationTestModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationTestModeId",
                unique: true,
                filter: "[PatientExaminationTestModeId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_PatientId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_RiskFactorAssesmentId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "RiskFactorAssesmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationRegularMode_DoctorId",
                schema: "oralHealthExamination",
                table: "PatientExaminationRegularMode",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationResult_APIBleedingId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                column: "APIBleedingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationResult_BeweId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                column: "BeweId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationResult_DMFT_DMFSId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                column: "DMFT_DMFSId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationTestMode_DoctorId",
                schema: "oralHealthExamination",
                table: "PatientExaminationTestMode",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationTestMode_StudentId",
                schema: "oralHealthExamination",
                table: "PatientExaminationTestMode",
                column: "StudentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PatientExaminationCard",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "PatientExaminationResult",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "PatientExaminationRegularMode",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "PatientExaminationTestMode",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "RiskFactorAssessment",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "APIBleeding",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "Bewe",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "DMFT_DMFS",
                schema: "oralHealthExamination");
        }
    }
}
