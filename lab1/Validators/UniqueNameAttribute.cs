using lab1.Data;
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
            if (name != null)
            {
                if (_model == "Course")
                {
                    var existingCourse = context.Courses.FirstOrDefault(c => c.crs_Name == name);
                    if (existingCourse != null)
                    {
                        return new ValidationResult("Name must be unique");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }
                else if (_model == "Student")
                {
                    var existingStudent = context.Students.FirstOrDefault(s => s.Name == name);
                    if (existingStudent != null)
                    {
                        return new ValidationResult("Name must be unique");
                    }
                    else
                    {
                        return ValidationResult.Success;
                    }
                }  
            }
            return new ValidationResult("Invalid name");
        }
    }
}
