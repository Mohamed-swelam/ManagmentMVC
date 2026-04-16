using lab1.Interfaces.IRepositories;
using lab1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace lab1.Controllers
{
    [Authorize(Roles ="Admin")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentRepo departmentRepo;

        public DepartmentController(IDepartmentRepo departmentRepo)
        {
            this.departmentRepo = departmentRepo;
        }



        public IActionResult Index()
        {
            var departments = departmentRepo.GetDepartmentsWithDetails().ToList();
            return View(departments);
        }

        public IActionResult Details(int id)
        {
            var department = departmentRepo.GetDepartmentWithDetails(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(Department department)
        {
            if (department.DeptName != null)
            {
                departmentRepo.Add(department);
                departmentRepo.Save();
                TempData["Success"] = "Department added successfully!";
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var department = departmentRepo.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            return View(department);
        }

        [HttpPost]
        public IActionResult Edit(Department department)
        {
            if (department.DeptName != null)
            {
                departmentRepo.Update(department);
                departmentRepo.Save();
                TempData["Success"] = "Department updated successfully!";
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult Delete(int id)
        {
            var department = departmentRepo.GetById(id);
            if (department == null)
            {
                return NotFound();
            }
            departmentRepo.Delete(department);
            departmentRepo.Save();
            TempData["Success"] = "Department deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
