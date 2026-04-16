using lab1.Interfaces.IRepositories;
using lab1.Models;
using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public IActionResult Index()
        {
            var courses = courseRepo.GetAll().ToList();
            return View(courses);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
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
