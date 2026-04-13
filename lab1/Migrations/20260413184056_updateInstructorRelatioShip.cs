using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab1.Migrations
{
    /// <inheritdoc />
    public partial class updateInstructorRelatioShip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ins_Courses_Courses_Coursecrs_Id",
                table: "Ins_Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Ins_Courses_Instructors_Instructorins_Id",
                table: "Ins_Courses");

            migrationBuilder.DropIndex(
                name: "IX_Ins_Courses_Coursecrs_Id",
                table: "Ins_Courses");

            migrationBuilder.DropIndex(
                name: "IX_Ins_Courses_Instructorins_Id",
                table: "Ins_Courses");

            migrationBuilder.DropColumn(
                name: "Coursecrs_Id",
                table: "Ins_Courses");

            migrationBuilder.DropColumn(
                name: "Instructorins_Id",
                table: "Ins_Courses");

            migrationBuilder.CreateIndex(
                name: "IX_Ins_Courses_crs_Id",
                table: "Ins_Courses",
                column: "crs_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ins_Courses_Courses_crs_Id",
                table: "Ins_Courses",
                column: "crs_Id",
                principalTable: "Courses",
                principalColumn: "crs_Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ins_Courses_Instructors_ins_Id",
                table: "Ins_Courses",
                column: "ins_Id",
                principalTable: "Instructors",
                principalColumn: "ins_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ins_Courses_Courses_crs_Id",
                table: "Ins_Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Ins_Courses_Instructors_ins_Id",
                table: "Ins_Courses");

            migrationBuilder.DropIndex(
                name: "IX_Ins_Courses_crs_Id",
                table: "Ins_Courses");

            migrationBuilder.AddColumn<int>(
                name: "Coursecrs_Id",
                table: "Ins_Courses",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Instructorins_Id",
                table: "Ins_Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Ins_Courses_Coursecrs_Id",
                table: "Ins_Courses",
                column: "Coursecrs_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ins_Courses_Instructorins_Id",
                table: "Ins_Courses",
                column: "Instructorins_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ins_Courses_Courses_Coursecrs_Id",
                table: "Ins_Courses",
                column: "Coursecrs_Id",
                principalTable: "Courses",
                principalColumn: "crs_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ins_Courses_Instructors_Instructorins_Id",
                table: "Ins_Courses",
                column: "Instructorins_Id",
                principalTable: "Instructors",
                principalColumn: "ins_Id");
        }
    }
}
