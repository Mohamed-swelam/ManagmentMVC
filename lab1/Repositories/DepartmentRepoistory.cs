using lab1.Data;
using lab1.Interfaces.IRepositories;
using lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace lab1.Repositories
{
    public class DepartmentRepoistory :Repository<Department>,IDepartmentRepo
    {
        private readonly AppDbContext context;

        public DepartmentRepoistory(AppDbContext context): base(context)
        {
            this.context = context;
        }

        public IEnumerable<Department> GetDepartmentsWithDetails()
        {
            return context.Departments.Include(e => e.Students)
                    .Include(e => e.Instructors);
        }

        public Department? GetDepartmentWithDetails(int id)
        {
            return context.Departments.Include(e => e.Students)
                    .Include(e => e.Instructors).FirstOrDefault(d => d.DeptId == id)??null;
        }
    }
}
