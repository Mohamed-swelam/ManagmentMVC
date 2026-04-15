using lab1.Interfaces.IRepositories;
using lab1.Models;
using lab1.ViewModels.StudCourseVM;
using Microsoft.AspNetCore.Mvc;

namespace lab1.Controllers
{
    public class StudCourseController : Controller
    {
        private readonly IStud_CoursesRepo stud_CoursesRepo;

        public StudCourseController(IStud_CoursesRepo stud_CoursesRepo)
        {
            this.stud_CoursesRepo = stud_CoursesRepo;
        }

        [HttpGet]
        public IActionResult AssignCourse(int id)
        {
            var courses = stud_CoursesRepo.GetCoursesAvaliableToAssignToStudent(id)
               .ToList();

            if (courses.Count == 0)
            {
                return RedirectToAction("Details", "Student", new { id = id });
            }
            else
            {
                ViewBag.StudentId = id;
                return View(courses);
            }

        }

        [HttpPost]
        public IActionResult AssignCourse(int studentId, int courseId)
        {
            var exists = stud_CoursesRepo.IsStudentEnrolledInCourse(studentId, courseId);

            if (!exists)
            {
                stud_CoursesRepo.Add(new Stud_Course
                {
                    SSN = studentId,
                    crs_Id = courseId,
                    Grade = 0
                });

                stud_CoursesRepo.Save();
                TempData["Success"] = "Course assigned successfully.";
            }

            return RedirectToAction("Details", "Student", new { id = studentId });
        }

        [HttpGet]
        public IActionResult EditGrade(int studentId)
        {
            var courses = stud_CoursesRepo.GetCoursesForStudent(studentId)
                .ToList();

            if (courses.Count == 0)
            {
                return RedirectToAction("Details", "Student", new { id = studentId });
            }
            ViewBag.studentId = studentId;
            return View(courses);

        }

        [HttpPost]
        public IActionResult EditGrade(EditGradeVM VM)
        {
            var course = stud_CoursesRepo.GetStudCourse(VM.studentId, VM.CourseId);

            if (course == null)
            {
                return NotFound();
            }
            else
            {
                course.Grade = VM.Grade;
                stud_CoursesRepo.Update(course);
                stud_CoursesRepo.Save();
                TempData["Success"] = "Grade updated successfully.";
                return RedirectToAction("Details", "Student", new { id = VM.studentId });
            }


        }
    }
}
