using lab1.Data;
using lab1.Interfaces.IRepositories;
using lab1.Models;

namespace lab1.Repositories
{
    public class CourseRepository : Repository<Course>, ICourseRepo
    {
        private readonly AppDbContext context;

        public CourseRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public Course? GetCourseByName(string name)
        {
            return context.Courses.FirstOrDefault(c => c.crs_Name == name) ?? null;
        }
    }
}
