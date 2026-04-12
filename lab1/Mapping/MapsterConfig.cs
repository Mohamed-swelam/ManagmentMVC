using lab1.Models;
using lab1.ViewModels.CourseVM;
using lab1.ViewModels.InstructorVM;
using lab1.ViewModels.StudentVM;
using Mapster;

namespace lab1.Mapping
{
    public class MapsterConfig
    {
        public static void RegisterMappings()
        {
            TypeAdapterConfig<Student, GetStudentWithFullDetialsVM>
                .NewConfig()
                .Map(dest => dest.DeptName,
                     src => src.Department.DeptName)
                .Map(dest => dest.Courses,
                     src => src.Stud_Courses);

            TypeAdapterConfig<Stud_Course, CourseWithDegreeVM>
                .NewConfig()
                .Map(dest => dest.CourseName,
                     src => src.Course.crs_Name)
                .Map(dest => dest.MinimumDegree,
                     src => src.Course.MinimumDegree)
                .Map(dest => dest.CourseId,src=>src.crs_Id);

            TypeAdapterConfig<Instructor, InstructorDetailsVM>
                .NewConfig()
                .Map(dest => dest.DeptName,
                     src => src.Department.DeptName)
                .Map(dest => dest.Courses,
                     src => src.ins_Courses);

            TypeAdapterConfig<Ins_Course, CourseWithHoursVM>
                .NewConfig()
                .Map(dest => dest.CourseName,
                     src => src.Course.crs_Name)
                .Map(dest => dest.Hours,
                     src => src.Hours)
                .Map(dest => dest.CourseId, src => src.crs_Id);
        }
    }
}
