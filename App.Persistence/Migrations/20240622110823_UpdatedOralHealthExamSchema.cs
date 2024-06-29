using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedOralHealthExamSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientExaminationCard_PatientExaminationRegularMode_Patien~",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientExaminationCard_PatientExaminationTestMode_PatientEx~",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropTable(
                name: "PatientExaminationRegularMode",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "PatientExaminationTestMode",
                schema: "oralHealthExamination");

            migrationBuilder.DropIndex(
                name: "IX_PatientExaminationCard_PatientExaminationRegularModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropIndex(
                name: "IX_PatientExaminationCard_PatientExaminationTestModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "PatientExaminationRegularModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "PatientExaminationTestModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfExamination",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DoctorId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRegularMode",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalScore",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "numeric(5,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_DoctorId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_StudentId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_PatientExaminationCard_ApplicationUser_DoctorId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "DoctorId",
                principalSchema: "user",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientExaminationCard_ApplicationUser_StudentId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "StudentId",
                principalSchema: "user",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientExaminationCard_ApplicationUser_DoctorId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientExaminationCard_ApplicationUser_StudentId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropIndex(
                name: "IX_PatientExaminationCard_DoctorId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropIndex(
                name: "IX_PatientExaminationCard_StudentId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "DateOfExamination",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "IsRegularMode",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "StudentId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.DropColumn(
                name: "TotalScore",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard");

            migrationBuilder.AddColumn<Guid>(
                name: "PatientExaminationRegularModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PatientExaminationTestModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                type: "uuid",
                nullable: true);

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
                    DoctorId = table.Column<string>(type: "text", nullable: true),
                    StudentId = table.Column<string>(type: "text", nullable: true),
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

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_PatientExaminationRegularModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationRegularModeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationCard_PatientExaminationTestModeId",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationTestModeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationRegularMode_DoctorId",
                schema: "oralHealthExamination",
                table: "PatientExaminationRegularMode",
                column: "DoctorId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PatientExaminationCard_PatientExaminationRegularMode_Patien~",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationRegularModeId",
                principalSchema: "oralHealthExamination",
                principalTable: "PatientExaminationRegularMode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientExaminationCard_PatientExaminationTestMode_PatientEx~",
                schema: "oralHealthExamination",
                table: "PatientExaminationCard",
                column: "PatientExaminationTestModeId",
                principalSchema: "oralHealthExamination",
                principalTable: "PatientExaminationTestMode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
