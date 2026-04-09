using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.Models
{
    public class Student
    {
        [Key]
        public int SSN { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
       
        public string? Image { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DeptId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Stud_Course> Stud_Courses { get; set; } = new HashSet<Stud_Course>();


    }
}
