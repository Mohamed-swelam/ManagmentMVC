using lab1.Data;
using lab1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab1.Controllers
{
    public class DepartmentController : Controller
    {
        AppDbContext context = new AppDbContext();

        public IActionResult Index()
        {
            var departments = context.Departments.Include(e => e.Students)
                    .Include(e => e.Instructors).ToList();
            return View(departments);
        }

        public IActionResult Details(int id)
        {
            var department = context.Departments.Include(e => e.Students)
                    .Include(e => e.Instructors).FirstOrDefault(d => d.DeptId == id);
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
                context.Departments.Add(department);
                context.SaveChanges();
                TempData["Success"] = "Department added successfully!";
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult Edit(int id)
        {
            var department = context.Departments.FirstOrDefault(d => d.DeptId == id);
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
                context.Departments.Update(department);
                context.SaveChanges();
                TempData["Success"] = "Department updated successfully!";
                return RedirectToAction("Index");
            }
            return View(department);
        }

        public IActionResult Delete(int id)
        {
            var department = context.Departments.FirstOrDefault(d => d.DeptId == id);
            if (department == null)
            {
                return NotFound();
            }
            context.Departments.Remove(department);
            context.SaveChanges();
            TempData["Success"] = "Department deleted successfully!";
            return RedirectToAction("Index");
        }
    }
}
