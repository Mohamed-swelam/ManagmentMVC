using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab1.Migrations
{
    /// <inheritdoc />
    public partial class AddSomeEntites : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeptId",
                table: "Students",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    crs_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    crs_Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Topics = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalDegree = table.Column<int>(type: "int", nullable: false),
                    MinimumDegree = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.crs_Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptId);
                });

            migrationBuilder.CreateTable(
                name: "Stud_Courses",
                columns: table => new
                {
                    SSN = table.Column<int>(type: "int", nullable: false),
                    crs_Id = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stud_Courses", x => new { x.SSN, x.crs_Id });
                    table.ForeignKey(
                        name: "FK_Stud_Courses_Courses_crs_Id",
                        column: x => x.crs_Id,
                        principalTable: "Courses",
                        principalColumn: "crs_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Stud_Courses_Students_SSN",
                        column: x => x.SSN,
                        principalTable: "Students",
                        principalColumn: "SSN",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Instructors",
                columns: table => new
                {
                    ins_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ins_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Salary = table.Column<double>(type: "float", nullable: false),
                    ins_Degree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ins_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ins_Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructors", x => x.ins_Id);
                    table.ForeignKey(
                        name: "FK_Instructors_Departments_DeptId",
                        column: x => x.DeptId,
                        principalTable: "Departments",
                        principalColumn: "DeptId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ins_Courses",
                columns: table => new
                {
                    ins_Id = table.Column<int>(type: "int", nullable: false),
                    crs_Id = table.Column<int>(type: "int", nullable: false),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Instructorins_Id = table.Column<int>(type: "int", nullable: true),
                    Coursecrs_Id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ins_Courses", x => new { x.ins_Id, x.crs_Id });
                    table.ForeignKey(
                        name: "FK_Ins_Courses_Courses_Coursecrs_Id",
                        column: x => x.Coursecrs_Id,
                        principalTable: "Courses",
                        principalColumn: "crs_Id");
                    table.ForeignKey(
                        name: "FK_Ins_Courses_Instructors_Instructorins_Id",
                        column: x => x.Instructorins_Id,
                        principalTable: "Instructors",
                        principalColumn: "ins_Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_DeptId",
                table: "Students",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Ins_Courses_Coursecrs_Id",
                table: "Ins_Courses",
                column: "Coursecrs_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Ins_Courses_Instructorins_Id",
                table: "Ins_Courses",
                column: "Instructorins_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Instructors_DeptId",
                table: "Instructors",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_Stud_Courses_crs_Id",
                table: "Stud_Courses",
                column: "crs_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DeptId",
                table: "Students",
                column: "DeptId",
                principalTable: "Departments",
                principalColumn: "DeptId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DeptId",
                table: "Students");

            migrationBuilder.DropTable(
                name: "Ins_Courses");

            migrationBuilder.DropTable(
                name: "Stud_Courses");

            migrationBuilder.DropTable(
                name: "Instructors");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Students_DeptId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "DeptId",
                table: "Students");
        }
    }
}
