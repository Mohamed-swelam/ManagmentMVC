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
            if (course.crs_Name != null)
            {
                context.Courses.Add(course);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(course);

        }
    }
}
