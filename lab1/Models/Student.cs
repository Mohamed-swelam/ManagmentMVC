using lab1.Validators;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.Models
{
    public class Student
    {
        [Key]
        public int SSN { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [MinLength(4, ErrorMessage = "Name must be at least 4 characters")]
        [MaxLength(15, ErrorMessage = "Name cannot exceed 15 characters")]
        //unique
        [UniqueName("Student")]
        public string Name { get; set; }
        
        [EmailAddress(ErrorMessage = "Invalid Email Format")]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        
        public string? Phone { get; set; }
        //between cairo ,alex,ismailia using remote
        [Remote(action: "ValidateAddress", controller: "Student",ErrorMessage ="Address must be between [Alexandria,Cairo,Ismailia]")]
        public string? Address { get; set; }
        [Range(12, 22, ErrorMessage = "Age must be between 12 and 22")]
        public int? Age { get; set; }

        //[RegularExpression(@"\w+\.(jpg|png)", ErrorMessage = "Image must be a .jpg or .png file")]
        public string? Image { get; set; }

        [NotMapped]
        public IFormFile? File { get; set; }

        [ForeignKey(nameof(Department))]
        public int? DeptId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Stud_Course> Stud_Courses { get; set; } = new HashSet<Stud_Course>();


    }
}
