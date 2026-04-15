using lab1.Data;
using lab1.Interfaces.IRepositories;
using lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace lab1.Repositories
{
    public class InstructorRepository : Repository<Instructor>, IInstructorRepo
    {
        private readonly AppDbContext context;

        public InstructorRepository(AppDbContext context) : base(context)
        {
            this.context = context;
        }

        public IEnumerable<Instructor> GetInstructorsWithDepartments()
        {
            return context.Instructors.Include(i => i.Department);
        }

        public Instructor? GetInstructorWithDepartment(int id)
        {
            return context.Instructors.Include(i => i.Department)
                .FirstOrDefault(i => i.ins_Id == id) ?? null;
        }

        public Instructor? GetInstructorWithDetails(int id)
        {
            return context.Instructors.Include(i => i.Department)
                .Include(i => i.ins_Courses)
                    .ThenInclude(ic => ic.Course)
                .FirstOrDefault(i => i.ins_Id == id) ?? null;
        }

        public Instructor? GetInstructorWithEmail(string email)
        {
            return context.Instructors.FirstOrDefault(i => i.ins_Email == email) ?? null;
        }
    }
}
