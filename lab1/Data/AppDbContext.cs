using lab1.Models;
using Microsoft.EntityFrameworkCore;

namespace lab1.Data
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = "Data Source=.;Initial Catalog=MVCITI;Integrated Security=True;Encrypt=True;Trust Server Certificate=True";
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ins_Course>()
                .HasKey(ic => new { ic.ins_Id, ic.crs_Id });

            modelBuilder.Entity<Stud_Course>()
                .HasKey(sc => new { sc.SSN, sc.crs_Id });
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Instructor> Instructors { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Ins_Course> Ins_Courses { get; set; }
        public DbSet<Stud_Course> Stud_Courses { get; set; }
    }
}
