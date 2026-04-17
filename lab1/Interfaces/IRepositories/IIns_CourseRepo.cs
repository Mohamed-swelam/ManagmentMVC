using lab1.Models;

namespace lab1.Interfaces.IRepositories
{
    public interface IIns_CourseRepo : IGenericRepo<Ins_Course>
    {
        IEnumerable<Course> GetCoursesAvaliableToAssignToInstructor(int ins_Id);
        bool IsInstructorAssignedToCourse(int ins_Id, int courseId);
        IEnumerable<Ins_Course> GetCoursesForInstructor(int ins_Id);
        Ins_Course? GetInsCourse(int ins_Id, int courseId);
    }
}
