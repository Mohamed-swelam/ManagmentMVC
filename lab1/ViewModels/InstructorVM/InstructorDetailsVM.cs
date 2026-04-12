using lab1.Models;
using lab1.Validators;
using lab1.ViewModels.CourseVM;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.ViewModels.InstructorVM
{
    public class InstructorDetailsVM
    {
        public int ins_Id { get; set; }
        public string ins_Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        public string? ins_Degree { get; set; }
        public string ins_Email { get; set; }
        public string? ins_Address { get; set; }
        public string? DeptName { get; set; }
        public List<CourseWithHoursVM> Courses { get; set; } = new List<CourseWithHoursVM>();

    }
}
