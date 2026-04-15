using lab1.Models;

namespace lab1.Interfaces.IRepositories
{
    public interface ICourseRepo : IGenericRepo<Course>
    {
        Course? GetCourseByName(string name);
    }
}
