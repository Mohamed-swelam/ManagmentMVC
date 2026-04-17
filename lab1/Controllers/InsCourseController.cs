using lab1.Interfaces.IRepositories;
using lab1.Models;
using lab1.ViewModels.InsCourseVM;
using Microsoft.AspNetCore.Mvc;

namespace lab1.Controllers
{
    public class InsCourseController : Controller
    {
        private readonly IIns_CourseRepo ins_CourseRepo;

        public InsCourseController(IIns_CourseRepo ins_CourseRepo)
        {
            this.ins_CourseRepo = ins_CourseRepo;
        }


        [HttpGet]
        public IActionResult AssignCourse(int id)
        {
            var courses = ins_CourseRepo.GetCoursesAvaliableToAssignToInstructor(id)
               .ToList();

            if (courses.Count == 0)
            {
                return RedirectToAction("Details", "Instructor", new { id = id });
            }
            else
            {
                ViewBag.InstructorId = id;
                return View(courses);
            }

        }

        [HttpPost]
        public IActionResult AssignCourse(int instructorId, int courseId)
        {
            var exists = ins_CourseRepo.IsInstructorAssignedToCourse(instructorId, courseId);

            if (!exists)
            {
                ins_CourseRepo.Add(new Ins_Course
                {
                    ins_Id = instructorId,
                    crs_Id = courseId,
                    Hours = 0
                });

                ins_CourseRepo.Save();
                TempData["Success"] = "Course assigned successfully.";
            }

            return RedirectToAction("Details", "Instructor", new { id = instructorId });
        }

        [HttpGet]
        public IActionResult EditHours(int instructorId)
        {
            var courses = ins_CourseRepo.GetCoursesForInstructor(instructorId)
                .ToList();

            if (courses.Count == 0)
            {
                return RedirectToAction("Details", "Instructor", new { id = instructorId });
            }
            ViewBag.InstructorId = instructorId;
            return View(courses);

        }

        [HttpPost]
        public IActionResult EditHours(EditHoursVM VM)
        {
            var course = ins_CourseRepo.GetInsCourse(VM.InstructorId, VM.CourseId);

            if (course == null)
            {
                return NotFound();
            }
            else
            {
                course.Hours = VM.Hours;
                ins_CourseRepo.Update(course);
                ins_CourseRepo.Save();
                TempData["Success"] = "Hours updated successfully.";
                return RedirectToAction("Details", "Instructor", new { id = VM.InstructorId });
            }


        }

        public IActionResult UnassignCourse(int instructorId, int courseId)
        {
            var course = ins_CourseRepo.GetInsCourse(instructorId, courseId);
            if (course == null)
            {
                return NotFound();
            }
            else
            {
                ins_CourseRepo.Delete(course);
                ins_CourseRepo.Save();
                TempData["Success"] = "Course removed successfully.";
                return RedirectToAction("Details", "Instructor", new { id = instructorId });
            }
        }
        }
}
