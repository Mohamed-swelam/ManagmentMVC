using lab1.Data;
using lab1.Models;
using System.ComponentModel.DataAnnotations;

namespace lab1.Validators
{
    public class UniqueNameAttribute : ValidationAttribute
    {
        AppDbContext context = new AppDbContext();
        private readonly string _model;

        public UniqueNameAttribute(string model)
        {
            _model = model;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var name = value as string;
            if (string.IsNullOrEmpty(name))
            {
                return new ValidationResult("Invalid name");
            }

            var instance = validationContext.ObjectInstance;

            if (_model == "Course")
            {
                var courseVM = instance as Course;

                var existingCourse = context.Courses
                    .FirstOrDefault(c => c.crs_Name == name);

                if (existingCourse != null)
                {
                    if (courseVM != null && existingCourse.crs_Id == courseVM.crs_Id)
                        return ValidationResult.Success;

                    return new ValidationResult("Name must be unique");
                }
            }
            else if (_model == "Student")
            {
                var studentVM = instance as Student;

                var existingStudent = context.Students
                    .FirstOrDefault(s => s.Name == name);

                if (existingStudent != null)
                {

                    if (studentVM != null && existingStudent.SSN == studentVM.SSN)
                        return ValidationResult.Success;

                    return new ValidationResult("Name must be unique");
                }
            }

            return ValidationResult.Success;
        }
    }

}
