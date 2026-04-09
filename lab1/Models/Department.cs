using System.ComponentModel.DataAnnotations;

namespace lab1.Models
{
    public class Department
    {
        [Key]
        public int DeptId { get; set; }
        [Required]
        [StringLength(100)]
        public string DeptName { get; set; }

        public string? Location { get; set; }
        public int? PhoneNumber { get; set; }

        public ICollection<Student> Students { get; set; } = new HashSet<Student>();
        public ICollection<Instructor> Instructors { get; set; } = new HashSet<Instructor>();
    }
}
