using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Course
    {
        [Key]
        public int crs_Id { get; set; }
        public string crs_Name { get; set; }
        public string Description { get; set; }
        public string? Topics { get; set; } 

        public int TotalDegree { get; set; }
        public int MinimumDegree { get; set; }
        public ICollection<Stud_Course> Stud_Courses { get; set; } = new HashSet<Stud_Course>();
        public ICollection<Ins_Course> Ins_Courses { get; set; } = new HashSet<Ins_Course>();
    }
}
