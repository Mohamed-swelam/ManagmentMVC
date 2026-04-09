using System.ComponentModel.DataAnnotations.Schema;

namespace lab1.Models
{
    public class Stud_Course
    {
        
        public int Grade { get; set; }

        [ForeignKey(nameof(Student))]
        public int SSN { get; set; }
        public Student Student { get; set; }

        [ForeignKey(nameof(Course))]
        public int crs_Id { get; set; }
        public Course Course { get; set; }

    }
}
