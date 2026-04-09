using lab1.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.ViewModels.Student
{
    public class GetStudentWithFullDetialsVM
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }

        public string? Image { get; set; }

        public string DeptName { get; set; }

        public List<Course>? Courses { get; set; }
    }
}
