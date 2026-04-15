using lab1.Interfaces.IRepositories;
using lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab1.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseRepo courseRepo;

        public CourseController(ICourseRepo courseRepo)
        {
            this.courseRepo = courseRepo;
        }

        public IActionResult Index()
        {
            var courses = courseRepo.GetAll().ToList();
            return View(courses);
        }


        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Course course)
        {
            if (ModelState.IsValid)
            {
                courseRepo.Add(course);
                courseRepo.Save();
                TempData["Success"] = "Course Added Successfully";
                return RedirectToAction("Index");
            }
            return View(course);

        }

        public IActionResult validateDegree(int TotalDegree, int MinimumDegree)
        {

            if (MinimumDegree > TotalDegree)
            {
                return Json(false);
            }
            else
            {
                return Json(true);
            }
        }
    }
}
