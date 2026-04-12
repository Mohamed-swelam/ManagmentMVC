using lab1.Data;
using lab1.Models;
using lab1.ViewModels.InstructorVM;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab1.Controllers
{
    public class InstructorController : Controller
    {

        AppDbContext context = new AppDbContext();



        public IActionResult Index()
        {
            var instructors = context.Instructors.Include(i => i.Department).ToList();
            return View(instructors);
        }

        public IActionResult Details(int id)
        {
            var instructor = context.Instructors.Include(i => i.Department)
                .Include(i => i.ins_Courses)
                    .ThenInclude(ic => ic.Course)
                .FirstOrDefault(i => i.ins_Id == id);
            if (instructor == null)
            {
                return NotFound();
            }

            var vm = instructor.Adapt<InstructorDetailsVM>();

            return View(vm);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Departments = context.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                context.Instructors.Add(instructor);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Departments = context.Departments.ToList();
                return View(instructor);
            }
        }




        public IActionResult Edit(int id)
        {
            var instructor = context.Instructors.Include(i => i.Department).FirstOrDefault(i => i.ins_Id == id);
            if (instructor == null)
            {
                return NotFound();
            }
            ViewBag.Departments = context.Departments.ToList();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                context.Instructors.Update(instructor);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Departments = context.Departments.ToList();
                return View(instructor);
            }
        }
    }
}
