using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "oralHealthExamination");

            migrationBuilder.EnsureSchema(
                name: "user");

            migrationBuilder.CreateTable(
                name: "APIBleeding",
                schema: "oralHealthExamination",
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
                    table.PrimaryKey("PK_APIBleeding", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationRole",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    NormalizedName = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationRole", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: false),
                    LastName = table.Column<string>(type: "character varying(225)", maxLength: 225, nullable: true),
                    IsAccountActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeleteUserComment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    GuestUserComment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    UserName = table.Column<string>(type: "text", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "text", nullable: true),
                    Email = table.Column<string>(type: "text", nullable: false),
                    NormalizedEmail = table.Column<string>(type: "text", nullable: true),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bewe",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BeweResult = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DMFTResult = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DMFSResult = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMFT_DMFS", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskFactorAssessment",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskFactorAssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskFactorAssessment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUserRole",
                schema: "user",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_ApplicationRole_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "user",
                        principalTable: "ApplicationRole",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRole_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Group",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    TeacherId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Group", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Group_ApplicationUser_TeacherId",
                        column: x => x.TeacherId,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientExaminationRegularMode",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorId = table.Column<string>(type: "text", nullable: true),
                    DateOfExamination = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: true),
                    DoctorId = table.Column<string>(type: "text", nullable: true),
                    DateOfExamination = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StudentMarks = table.Column<decimal>(type: "numeric", nullable: false)
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
                name: "RefreshToken",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: true),
                    Token = table.Column<string>(type: "text", nullable: true),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Revoked = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshToken", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshToken_ApplicationUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ResearchGroup",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GroupName = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    DoctorId = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchGroup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResearchGroup_ApplicationUser_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserRequests",
                schema: "user",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    RequestTitle = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AdminComment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    RequestStatus = table.Column<int>(type: "integer", maxLength: 20, nullable: false),
                    DateSubmitted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateCompleted = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRequests_ApplicationUser_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PatientExaminationResult",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BeweId = table.Column<Guid>(type: "uuid", nullable: false),
                    DMFT_DMFSId = table.Column<Guid>(type: "uuid", nullable: false),
                    APIBleedingId = table.Column<Guid>(type: "uuid", nullable: false)
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
                name: "StudentGroup",
                schema: "user",
                columns: table => new
                {
                    GroupId = table.Column<Guid>(type: "uuid", nullable: false),
                    StudentId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroup", x => new { x.GroupId, x.StudentId });
                    table.ForeignKey(
                        name: "FK_StudentGroup_ApplicationUser_StudentId",
                        column: x => x.StudentId,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StudentGroup_Group_GroupId",
                        column: x => x.GroupId,
                        principalSchema: "user",
                        principalTable: "Group",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                schema: "oralHealthExamination",
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
                    Age = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsArchived = table.Column<bool>(type: "boolean", nullable: false),
                    ArchiveComment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    DoctorId = table.Column<string>(type: "text", nullable: true),
                    ResearchGroupId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patient_ApplicationUser_DoctorId",
                        column: x => x.DoctorId,
                        principalSchema: "user",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Patient_ResearchGroup_ResearchGroupId",
                        column: x => x.ResearchGroupId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "ResearchGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "PatientExaminationCard",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DoctorComment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    PatientId = table.Column<Guid>(type: "uuid", nullable: false),
                    RiskFactorAssesmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    PatientExaminationRegularModeId = table.Column<Guid>(type: "uuid", nullable: true),
                    PatientExaminationTestModeId = table.Column<Guid>(type: "uuid", nullable: true),
                    PatientExaminationResultId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PatientExaminationCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PatientExaminationCard_PatientExaminationRegularMode_Patien~",
                        column: x => x.PatientExaminationRegularModeId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "PatientExaminationRegularMode",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientExaminationCard_PatientExaminationResult_PatientExam~",
                        column: x => x.PatientExaminationResultId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "PatientExaminationResult",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PatientExaminationCard_PatientExaminationTestMode_PatientEx~",
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
                        name: "FK_PatientExaminationCard_RiskFactorAssessment_RiskFactorAsses~",
                        column: x => x.RiskFactorAssesmentId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "RiskFactorAssessment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRole_RoleId",
                schema: "user",
                table: "ApplicationUserRole",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Group_TeacherId",
                schema: "user",
                table: "Group",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_DoctorId",
                schema: "oralHealthExamination",
                table: "Patient",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Patient_Email",
                schema: "oralHealthExamination",
                table: "Patient",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Patient_ResearchGroupId",
                schema: "oralHealthExamination",
                table: "Patient",
                column: "ResearchGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_PatientExaminationRegularModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationRegularModeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_PatientExaminationResultId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationResultId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_PatientExaminationTestModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationTestModeId",
                unique: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_RefreshToken_UserId",
                schema: "user",
                table: "RefreshToken",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchGroup_DoctorId",
                schema: "oralHealthExamination",
                table: "ResearchGroup",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroup_StudentId",
                schema: "user",
                table: "StudentGroup",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRequests_CreatedBy",
                schema: "user",
                table: "UserRequests",
                column: "CreatedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserRole",
                schema: "user");

            migrationBuilder.DropTable(
                name: "PatientExaminationCard",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "RefreshToken",
                schema: "user");

            migrationBuilder.DropTable(
                name: "StudentGroup",
                schema: "user");

            migrationBuilder.DropTable(
                name: "UserRequests",
                schema: "user");

            migrationBuilder.DropTable(
                name: "ApplicationRole",
                schema: "user");

            migrationBuilder.DropTable(
                name: "PatientExaminationRegularMode",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "PatientExaminationResult",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "PatientExaminationTestMode",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "Patient",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "RiskFactorAssessment",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "Group",
                schema: "user");

            migrationBuilder.DropTable(
                name: "APIBleeding",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "Bewe",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "DMFT_DMFS",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "ResearchGroup",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "ApplicationUser",
                schema: "user");
        }
    }
}
