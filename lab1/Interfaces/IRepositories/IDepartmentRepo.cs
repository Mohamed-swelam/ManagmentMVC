using lab1.Models;

namespace lab1.Interfaces.IRepositories
{
    public interface IDepartmentRepo : IGenericRepo<Department>
    {
        IEnumerable<Department> GetDepartmentsWithDetails();
        Department? GetDepartmentWithDetails(int id);
    }
}
