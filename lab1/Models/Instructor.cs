using lab1.Validators;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.Models
{
    public class Instructor
    {
        [Key]
        public int ins_Id { get; set; }
        [Required]
        [MaxLength(30,ErrorMessage ="Name cannot exceed 30 characters")]
        public string ins_Name { get; set; }
        public int Age { get; set; }
        [Range(5000,15000,ErrorMessage ="Salary must be between 5000 and 15000")]
        public double Salary { get; set; }
        //degree
        public string? ins_Degree { get; set; }
        [EmailAddress]
        [Required(ErrorMessage ="Email is required")]
        [UniqueEmail(ErrorMessage = "There is an Instructor with this Email")]
        public string ins_Email { get; set; }
        public string? ins_Address { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DeptId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Ins_Course> ins_Courses { get; set; } = new HashSet<Ins_Course>();
    }
}
