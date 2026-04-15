using lab1.Models;

namespace lab1.Interfaces.IRepositories
{
    public interface IInstructorRepo : IGenericRepo<Instructor>
    {
        IEnumerable<Instructor> GetInstructorsWithDepartments();
        Instructor? GetInstructorWithDetails(int id);
        Instructor? GetInstructorWithDepartment(int id);
        Instructor? GetInstructorWithEmail(string email);
    }
}
