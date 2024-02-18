using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreditSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "credit");

            migrationBuilder.CreateTable(
                name: "Exam",
                schema: "credit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfExamination = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ExamTitle = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PublishDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StartTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    EndTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    Duration = table.Column<int>(type: "integer", nullable: false),
                    MaxMark = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exam", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticeAPIBleeding",
                schema: "credit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    APIResult = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    BleedingResult = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Comments = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeAPIBleeding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticeBewe",
                schema: "credit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BeweResult = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeBewe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticeDMFT_DMFS",
                schema: "credit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DMFTResult = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DMFSResult = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeDMFT_DMFS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticePatient",
                schema: "credit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "text", nullable: false),
                    EthnicGroup = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    OtherGroup = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    YearsInSchool = table.Column<int>(type: "integer", nullable: false),
                    OtherData = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OtherData2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    OtherData3 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticePatient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticeRiskFactorAssessment",
                schema: "credit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskFactorAssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeRiskFactorAssessment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroupExam",
                schema: "credit",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExamId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroupExam", x => new { x.GroupId, x.ExamId });
                    table.ForeignKey(
                        name: "FK_GroupExam_Exam_ExamId",
                        column: x => x.ExamId,
                        principalSchema: "credit",
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GroupExam_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "user",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PracticePatientExaminationResult",
                schema: "credit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BeweId = table.Column<Guid>(type: "uuid", nullable: false),
                    DMFT_DMFSId = table.Column<Guid>(type: "uuid", nullable: false),
                    APIBleedingId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticePatientExaminationResult", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticePatientExaminationResult_PracticeAPIBleeding_APIBle~",
                        column: x => x.APIBleedingId,
                        principalSchema: "credit",
                        principalTable: "PracticeAPIBleeding",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticePatientExaminationResult_PracticeBewe_BeweId",
                        column: x => x.BeweId,
                        principalSchema: "credit",
                        principalTable: "PracticeBewe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticePatientExaminationResult_PracticeDMFT_DMFS_DMFT_DMF~",
                        column: x => x.DMFT_DMFSId,
                        principalSchema: "credit",
                        principalTable: "PracticeDMFT_DMFS",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PracticePatientExaminationCard",
                schema: "credit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentMark = table.Column<decimal>(type: "numeric(5,2)", precision: 5, scale: 2, nullable: false),
                    DoctorComment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RiskFactorAssessmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientExaminationResultId = table.Column<Guid>(type: "uuid", nullable: false),
                    ExamId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticePatientExaminationCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PracticePatientExaminationCard_ApplicationUser_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticePatientExaminationCard_Exam_ExamId",
                        column: x => x.ExamId,
                        principalSchema: "credit",
                        principalTable: "Exam",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticePatientExaminationCard_PracticePatientExaminationRe~",
                        column: x => x.PatientExaminationResultId,
                        principalSchema: "credit",
                        principalTable: "PracticePatientExaminationResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticePatientExaminationCard_PracticePatient_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "credit",
                        principalTable: "PracticePatient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PracticePatientExaminationCard_PracticeRiskFactorAssessment~",
                        column: x => x.RiskFactorAssessmentId,
                        principalSchema: "credit",
                        principalTable: "PracticeRiskFactorAssessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GroupExam_ExamId",
                schema: "credit",
                table: "GroupExam",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatient_Email",
                schema: "credit",
                table: "PracticePatient",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatientExaminationCard_ExamId",
                schema: "credit",
                table: "PracticePatientExaminationCard",
                column: "ExamId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatientExaminationCard_PatientExaminationResultId",
                schema: "credit",
                table: "PracticePatientExaminationCard",
                column: "PatientExaminationResultId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatientExaminationCard_PatientId",
                schema: "credit",
                table: "PracticePatientExaminationCard",
                column: "PatientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatientExaminationCard_RiskFactorAssessmentId",
                schema: "credit",
                table: "PracticePatientExaminationCard",
                column: "RiskFactorAssessmentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatientExaminationCard_StudentId",
                schema: "credit",
                table: "PracticePatientExaminationCard",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatientExaminationResult_APIBleedingId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                column: "APIBleedingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatientExaminationResult_BeweId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                column: "BeweId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatientExaminationResult_DMFT_DMFSId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                column: "DMFT_DMFSId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupExam",
                schema: "credit");

            migrationBuilder.DropTable(
                name: "PracticePatientExaminationCard",
                schema: "credit");

            migrationBuilder.DropTable(
                name: "Exam",
                schema: "credit");

            migrationBuilder.DropTable(
                name: "PracticePatientExaminationResult",
                schema: "credit");

            migrationBuilder.DropTable(
                name: "PracticePatient",
                schema: "credit");

            migrationBuilder.DropTable(
                name: "PracticeRiskFactorAssessment",
                schema: "credit");

            migrationBuilder.DropTable(
                name: "PracticeAPIBleeding",
                schema: "credit");

            migrationBuilder.DropTable(
                name: "PracticeBewe",
                schema: "credit");

            migrationBuilder.DropTable(
                name: "PracticeDMFT_DMFS",
                schema: "credit");
        }
    }
}
