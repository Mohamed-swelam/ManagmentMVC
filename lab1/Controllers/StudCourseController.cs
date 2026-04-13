using lab1.Data;
using lab1.Models;
using lab1.ViewModels.StudCourseVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab1.Controllers
{
    public class StudCourseController : Controller
    {
        AppDbContext context = new AppDbContext();

        [HttpGet]
        public IActionResult AssignCourse(int id)
        {
            var courses = context.Courses
               .Where(c => !context.Stud_Courses
                   .Any(sc => sc.SSN == id && sc.crs_Id == c.crs_Id))
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
            var exists = context.Stud_Courses
                .Any(sc => sc.SSN == studentId && sc.crs_Id == courseId);

            if (!exists)
            {
                context.Stud_Courses.Add(new Stud_Course
                {
                    SSN = studentId,
                    crs_Id = courseId,
                    Grade = 0
                });

                context.SaveChanges();
                TempData["Success"] = "Course assigned successfully.";
            }

            return RedirectToAction("Details", "Student", new { id = studentId });
        }

        [HttpGet]
        public IActionResult EditGrade(int studentId)
        {
            var courses = context.Stud_Courses
                .Where(sc => sc.SSN == studentId)
                .Include(sc => sc.Course)
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
            var course = context.Stud_Courses
                .FirstOrDefault(sc => sc.SSN == VM.studentId && sc.crs_Id == VM.CourseId);

            if (course == null)
            {
                return NotFound();
            }
            else
            {
                course.Grade = VM.Grade;
                context.SaveChanges();
                //TempData["Success"]
                TempData["Success"] = "Grade updated successfully.";
                return RedirectToAction("Details", "Student", new { id = VM.studentId });
            }


        }
    }
}
