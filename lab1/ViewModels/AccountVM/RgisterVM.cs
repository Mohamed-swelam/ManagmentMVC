using System.ComponentModel.DataAnnotations;

namespace lab1.ViewModels.AccountVM
{
    public class RgisterVM
    {
        [Required(ErrorMessage ="Email is Required")]
        [EmailAddress(ErrorMessage ="Invalid Email Format")]
        public string Email { get; set; }
        [Display(Name = "First Name")]
        [Required(ErrorMessage ="First Name is Required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Last Name is Required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage ="Password is Required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage ="Confirm Password is Required")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage ="Age is Required")]
        [Range(8, 60, ErrorMessage = "Age must be between 8 and 60.")]
        public int Age { get; set; }
        public string? Address { get; set; }
    }
}
