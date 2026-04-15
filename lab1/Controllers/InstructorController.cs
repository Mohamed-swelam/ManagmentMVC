using lab1.Interfaces.IRepositories;
using lab1.Models;
using lab1.ViewModels.InstructorVM;
using Mapster;
using Microsoft.AspNetCore.Mvc;

namespace lab1.Controllers
{
    public class InstructorController : Controller
    {
        private readonly IInstructorRepo instructorRepo;
        private readonly IDepartmentRepo departmentRepo;

        public InstructorController(IInstructorRepo instructorRepo, IDepartmentRepo departmentRepo)
        {
            this.instructorRepo = instructorRepo;
            this.departmentRepo = departmentRepo;
        }



        public IActionResult Index()
        {
            var instructors = instructorRepo.GetInstructorsWithDepartments().ToList();
            return View(instructors);
        }

        public IActionResult Details(int id)
        {
            var instructor = instructorRepo.GetInstructorWithDetails(id);
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
            ViewBag.Departments = departmentRepo.GetAll().ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Add(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                instructorRepo.Add(instructor);
                instructorRepo.Save();
                TempData["Success"] = "Instructor added successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Departments = departmentRepo.GetAll().ToList();
                return View(instructor);
            }
        }




        public IActionResult Edit(int id)
        {
            var instructor = instructorRepo.GetInstructorWithDepartment(id);
            if (instructor == null)
            {
                return NotFound();
            }
            ViewBag.Departments = departmentRepo.GetAll().ToList();
            return View(instructor);
        }

        [HttpPost]
        public IActionResult Edit(Instructor instructor)
        {
            if (ModelState.IsValid)
            {
                instructorRepo.Update(instructor);
                instructorRepo.Save();
                TempData["Success"] = "Instructor updated successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Departments = departmentRepo.GetAll().ToList();
                return View(instructor);
            }
        }

        public IActionResult Delete(int id)
        {
            var instructor = instructorRepo.GetById(id);

            if (instructor == null)
                return NotFound();

            instructorRepo.Delete(instructor);
            instructorRepo.Save();

            TempData["Success"] = "Instructor deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
