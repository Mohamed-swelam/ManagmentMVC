using lab1.Data;
using lab1.Interfaces.IRepositories;
using lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace lab1.Repositories
{
    public class StudentRepository : Repository<Student>, IStudentRepo
    {
        private readonly AppDbContext context;

        public StudentRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public Student? GetStudentByName(string name)
        {
            return context.Students.FirstOrDefault(s => s.Name == name) ?? null;
        }

        public Student? GetStudentWithFullDetails(int id)
        {
           return context.Students.Include(s => s.Department)
                .Include(s => s.Stud_Courses)
                .ThenInclude(sc => sc.Course)
                .FirstOrDefault(s => s.SSN == id) ?? null;
        }
    }
}
