using lab1.Data;
using lab1.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab1.Controllers
{
    public class CourseController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult Index()
        {
            var courses = context.Courses.ToList();
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
                context.Courses.Add(course);
                context.SaveChanges();
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
