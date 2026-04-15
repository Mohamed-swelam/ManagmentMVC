using lab1.Models;

namespace lab1.Interfaces.IRepositories
{
    public interface IStudentRepo : IGenericRepo<Student>
    {
        Student? GetStudentWithFullDetails(int id);
        Student? GetStudentByName(string name);
    }
}
