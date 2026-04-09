using AutoMapper;
using lab1.Models;
using lab1.ViewModels.CourseVM;
using lab1.ViewModels.StudentVM;

namespace lab1.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Student, GetStudentWithFullDetialsVM>()
                .ForMember(dest => dest.DeptName,
                    opt => opt.MapFrom(src => src.Department.DeptName))

                .ForMember(dest => dest.Courses,
                    opt => opt.MapFrom(src => src.Stud_Courses));

            CreateMap<Stud_Course, CourseWithDegreeVM>()
                .ForMember(dest => dest.CourseName,
                    opt => opt.MapFrom(src => src.Course.crs_Name))
                .ForMember(dest => dest.MinimumDegree,
                    opt => opt.MapFrom(src => src.Course.MinimumDegree));
        }
    }
}
