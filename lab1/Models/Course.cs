using lab1.Validators;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Course
    {
        [Key]
        public int crs_Id { get; set; }
        [Required]
        [UniqueName("Course")]
        public string crs_Name { get; set; }
        public string Description { get; set; }
        public string? Topics { get; set; }
        [Range(0, 500, ErrorMessage = "Degree must be between 0 and 500")]
        public int TotalDegree { get; set; }
        [Remote(action: "validateDegree", controller: "Course", AdditionalFields = nameof(TotalDegree), ErrorMessage = "Minimum Degree must be between 0 and Total Degree")]
        public int MinimumDegree { get; set; }
        public ICollection<Stud_Course> Stud_Courses { get; set; } = new HashSet<Stud_Course>();
        public ICollection<Ins_Course> Ins_Courses { get; set; } = new HashSet<Ins_Course>();
    }
}
