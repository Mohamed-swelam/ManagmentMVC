using lab1.Data;
using lab1.Interfaces.IRepositories;
using lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace lab1.Repositories
{
    public class Stud_CoursesRepository : Repository<Stud_Course>, IStud_CoursesRepo
    {
        private readonly AppDbContext context;

        public Stud_CoursesRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Course> GetCoursesAvaliableToAssignToStudent(int studentId)
        {
            return context.Courses
               .Where(c => !context.Stud_Courses
                   .Any(sc => sc.SSN == studentId && sc.crs_Id == c.crs_Id));
        }

        public IEnumerable<Stud_Course> GetCoursesForStudent(int studentId)
        {
            return context.Stud_Courses
                .Where(sc => sc.SSN == studentId)
                .Include(sc => sc.Course);
        }

        public Stud_Course? GetStudCourse(int studentId, int courseId)
        {
           return context.Stud_Courses
                .FirstOrDefault(sc => sc.SSN == studentId && sc.crs_Id == courseId)??null;
        }

        public bool IsStudentEnrolledInCourse(int studentId, int courseId)
        {
            return context.Stud_Courses
                .Any(sc => sc.SSN == studentId && sc.crs_Id == courseId);
        }
    }
}
