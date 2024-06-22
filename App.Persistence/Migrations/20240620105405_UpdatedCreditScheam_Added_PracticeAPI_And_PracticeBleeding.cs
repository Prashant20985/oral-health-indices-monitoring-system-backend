using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCreditScheam_Added_PracticeAPI_And_PracticeBleeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PracticePatientExaminationResult_PracticeAPIBleeding_APIBle~",
                schema: "credit",
                table: "PracticePatientExaminationResult");

            migrationBuilder.DropTable(
                name: "PracticeAPIBleeding",
                schema: "credit");

            migrationBuilder.RenameColumn(
                name: "APIBleedingId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                newName: "BleedingId");

            migrationBuilder.RenameIndex(
                name: "IX_PracticePatientExaminationResult_APIBleedingId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                newName: "IX_PracticePatientExaminationResult_BleedingId");

            migrationBuilder.AddColumn<Guid>(
                name: "APIId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "PracticeAPI",
                schema: "credit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    APIResult = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeAPI", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PracticeBleeding",
                schema: "credit",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BleedingResult = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    AssessmentModel = table.Column<string>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PracticeBleeding", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatientExaminationResult_APIId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                column: "APIId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PracticePatientExaminationResult_PracticeAPI_APIId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                column: "APIId",
                principalSchema: "credit",
                principalTable: "PracticeAPI",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PracticePatientExaminationResult_PracticeBleeding_BleedingId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                column: "BleedingId",
                principalSchema: "credit",
                principalTable: "PracticeBleeding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PracticePatientExaminationResult_PracticeAPI_APIId",
                schema: "credit",
                table: "PracticePatientExaminationResult");

            migrationBuilder.DropForeignKey(
                name: "FK_PracticePatientExaminationResult_PracticeBleeding_BleedingId",
                schema: "credit",
                table: "PracticePatientExaminationResult");

            migrationBuilder.DropTable(
                name: "PracticeAPI",
                schema: "credit");

            migrationBuilder.DropTable(
                name: "PracticeBleeding",
                schema: "credit");

            migrationBuilder.DropIndex(
                name: "IX_PracticePatientExaminationResult_APIId",
                schema: "credit",
                table: "PracticePatientExaminationResult");

            migrationBuilder.DropColumn(
                name: "APIId",
                schema: "credit",
                table: "PracticePatientExaminationResult");

            migrationBuilder.RenameColumn(
                name: "BleedingId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                newName: "APIBleedingId");

            migrationBuilder.RenameIndex(
                name: "IX_PracticePatientExaminationResult_BleedingId",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                newName: "IX_PracticePatientExaminationResult_APIBleedingId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_PracticePatientExaminationResult_PracticeAPIBleeding_APIBle~",
                schema: "credit",
                table: "PracticePatientExaminationResult",
                column: "APIBleedingId",
                principalSchema: "credit",
                principalTable: "PracticeAPIBleeding",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
