using lab1.Data;
using lab1.Interfaces.IRepositories;
using lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace lab1.Repositories
{
    public class Ins_CourseRepository : Repository<Ins_Course>, IIns_CourseRepo
    {
        private readonly AppDbContext context;

        public Ins_CourseRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Course> GetCoursesAvaliableToAssignToInstructor(int ins_Id)
        {
            return context.Courses
                .Where(c => !context.Ins_Courses.Any(ic => ic.crs_Id == c.crs_Id && ic.ins_Id == ins_Id))
                .ToList();
        }

        public IEnumerable<Ins_Course> GetCoursesForInstructor(int ins_Id)
        {
            return context.Ins_Courses
                .Where(ic => ic.ins_Id == ins_Id)
                .Include(ic => ic.Course) // Include course details
                .ToList();
        }

        public Ins_Course? GetInsCourse(int ins_Id, int courseId)
        {
            return context.Ins_Courses
                .FirstOrDefault(ic => ic.ins_Id == ins_Id && ic.crs_Id == courseId);
        }

        public bool IsInstructorAssignedToCourse(int ins_Id, int courseId)
        {
            return context.Ins_Courses.Any(ic => ic.ins_Id == ins_Id && ic.crs_Id == courseId);
        }
    }
}
