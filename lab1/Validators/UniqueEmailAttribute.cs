using lab1.Data;
using lab1.Models;
using lab1.ViewModels.InstructorVM;
using System.ComponentModel.DataAnnotations;

namespace lab1.Validators
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            AppDbContext context = new AppDbContext();

            var email = value as string;
            if (string.IsNullOrEmpty(email))
            {
                return new ValidationResult("Email is invalid");
            }

            var instructor = validationContext.ObjectInstance as Instructor;
                

            var existingInstructor = context.Instructors
                .FirstOrDefault(i => i.ins_Email == email);

            if (existingInstructor != null)
            {
                if (instructor != null && existingInstructor.ins_Id == instructor.ins_Id)
                {
                    return ValidationResult.Success;
                }

                return new ValidationResult("Email already exists");
            }

            return ValidationResult.Success;
        }
    }
}
