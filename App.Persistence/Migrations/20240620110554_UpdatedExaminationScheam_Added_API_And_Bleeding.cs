using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedExaminationScheam_Added_API_And_Bleeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientExaminationResult_APIBleeding_APIBleedingId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult");

            migrationBuilder.DropTable(
                name: "APIBleeding",
                schema: "oralHealthExamination");

            migrationBuilder.RenameColumn(
                name: "APIBleedingId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                newName: "BleedingId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientExaminationResult_APIBleedingId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                newName: "IX_PatientExaminationResult_BleedingId");

            migrationBuilder.AddColumn<Guid>(
                name: "APIId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "API",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    APIResult = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_API", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bleeding",
                schema: "oralHealthExamination",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BleedingResult = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bleeding", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PatientExaminationResult_APIId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                column: "APIId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientExaminationResult_API_APIId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                column: "APIId",
                principalSchema: "oralHealthExamination",
                principalTable: "API",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PatientExaminationResult_Bleeding_BleedingId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                column: "BleedingId",
                principalSchema: "oralHealthExamination",
                principalTable: "Bleeding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PatientExaminationResult_API_APIId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult");

            migrationBuilder.DropForeignKey(
                name: "FK_PatientExaminationResult_Bleeding_BleedingId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult");

            migrationBuilder.DropTable(
                name: "API",
                schema: "oralHealthExamination");

            migrationBuilder.DropTable(
                name: "Bleeding",
                schema: "oralHealthExamination");

            migrationBuilder.DropIndex(
                name: "IX_PatientExaminationResult_APIId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult");

            migrationBuilder.DropColumn(
                name: "APIId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult");

            migrationBuilder.RenameColumn(
                name: "BleedingId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                newName: "APIBleedingId");

            migrationBuilder.RenameIndex(
                name: "IX_PatientExaminationResult_BleedingId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                newName: "IX_PatientExaminationResult_APIBleedingId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PatientExaminationResult_APIBleeding_APIBleedingId",
                schema: "oralHealthExamination",
                table: "PatientExaminationResult",
                column: "APIBleedingId",
                principalSchema: "oralHealthExamination",
                principalTable: "APIBleeding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
