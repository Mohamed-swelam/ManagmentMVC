using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.Models
{
    public class Instructor
    {
        [Key]
        public int ins_Id { get; set; }
        [Required]
        [StringLength(100)]
        public string ins_Name { get; set; }
        public int Age { get; set; }
        public double Salary { get; set; }
        //degree
        public string? ins_Degree { get; set; }
        [EmailAddress]
        [Required]
        public string ins_Email { get; set; }
        public string? ins_Address { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DeptId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Ins_Course> ins_Courses { get; set; } = new HashSet<Ins_Course>();
    }
}
