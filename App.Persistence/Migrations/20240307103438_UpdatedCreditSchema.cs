using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace App.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdatedCreditSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GroupExam",
                schema: "credit");

            migrationBuilder.DropIndex(
                name: "IX_PracticePatient_Email",
                schema: "credit",
                table: "PracticePatient");

            migrationBuilder.AddColumn<string>(
                name: "ExamStatus",
                schema: "credit",
                table: "Exam",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "GroupId",
                schema: "credit",
                table: "Exam",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Exam_GroupId",
                schema: "credit",
                table: "Exam",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exam_Group_GroupId",
                schema: "credit",
                table: "Exam",
                column: "GroupId",
                principalSchema: "user",
                principalTable: "Group",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exam_Group_GroupId",
                schema: "credit",
                table: "Exam");

            migrationBuilder.DropIndex(
                name: "IX_Exam_GroupId",
                schema: "credit",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "ExamStatus",
                schema: "credit",
                table: "Exam");

            migrationBuilder.DropColumn(
                name: "GroupId",
                schema: "credit",
                table: "Exam");

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

            migrationBuilder.CreateIndex(
                name: "IX_PracticePatient_Email",
                schema: "credit",
                table: "PracticePatient",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GroupExam_ExamId",
                schema: "credit",
                table: "GroupExam",
                column: "ExamId");
        }
    }
}
