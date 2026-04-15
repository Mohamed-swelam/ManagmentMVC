using lab1.Data;
using lab1.Interfaces.IRepositories;
using lab1.Models;
using lab1.ViewModels.InstructorVM;
using System.ComponentModel.DataAnnotations;

namespace lab1.Validators
{
    public class UniqueEmailAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {

            var email = value as string;
            var repo = validationContext.GetService(typeof(IInstructorRepo)) as IInstructorRepo;
            if (string.IsNullOrEmpty(email))
            {
                return new ValidationResult("Email is invalid");
            }

            var instructor = validationContext.ObjectInstance as Instructor;
                

            var existingInstructor = repo.GetInstructorWithEmail(email);

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
