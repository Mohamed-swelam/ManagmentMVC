using lab1.Models;

namespace lab1.Interfaces.IRepositories
{
    public interface IStud_CoursesRepo : IGenericRepo<Stud_Course>
    {
        IEnumerable<Course> GetCoursesAvaliableToAssignToStudent(int studentId);
        bool IsStudentEnrolledInCourse(int studentId, int courseId);
        IEnumerable<Stud_Course> GetCoursesForStudent(int studentId);
        Stud_Course? GetStudCourse(int studentId, int courseId);
    }
}
