using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations.OralHealthExaminationSchema
{
    /// <inheritdoc />
    public partial class InitialOralHealthExaminationSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "oralHealthExamination");

            migrationBuilder.CreateTable(
                name: "ResearchGroup",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                name: "Patient",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EthnicGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    OtherGroup = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    YearsInSchool = table.Column<int>(type: "int", nullable: false),
                    OtherData = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OtherData2 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    OtherData3 = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    IsArchived = table.Column<bool>(type: "bit", nullable: false),
                    ArchiveComment = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    DoctorId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    PatientGroupId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
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
                        name: "FK_Patient_ResearchGroup_PatientGroupId",
                        column: x => x.PatientGroupId,
                        principalSchema: "oralHealthExamination",
                        principalTable: "ResearchGroup",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

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
                name: "IX_Patient_PatientGroupId",
                schema: "oralHealthExamination",
                table: "Patient",
                column: "PatientGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_ResearchGroup_DoctorId",
                schema: "oralHealthExamination",
                table: "ResearchGroup",
                column: "DoctorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patient",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "ResearchGroup",
                schema: "oralHealthExamination");
        }
    }
}
