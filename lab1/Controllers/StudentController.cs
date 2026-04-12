using lab1.Data;
using lab1.Models;
using lab1.ViewModels.CourseVM;
using lab1.ViewModels.StudentVM;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab1.Controllers
{
    public class StudentController : Controller
    {
        AppDbContext _dbContext = new AppDbContext();
        private readonly IWebHostEnvironment webHostEnvironment;

        public StudentController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            var students = _dbContext.Students.ToList();
            return View("Index", students);
        }

        public IActionResult Details(int id)
        {
            var student = _dbContext.Students
                .Include(s => s.Department)
                .Include(s => s.Stud_Courses)
                .ThenInclude(sc => sc.Course)
                .FirstOrDefault(s => s.SSN == id);

            var vm = student.Adapt<GetStudentWithFullDetialsVM>();
            return View("Details", vm);
        }

        public IActionResult Add()
        {
            ViewBag.Departments = _dbContext.Departments.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult AddStudent(Student student)
        {
            if (student.File != null)
            {
                var ext = Path.GetExtension(student.File.FileName).ToLower();

                if (ext != ".jpg" && ext != ".png")
                {
                    ModelState.AddModelError("File", "Only .jpg or .png files are allowed");
                }
            }

            if (ModelState.IsValid)
            {
                string Rootfile = webHostEnvironment.WebRootPath;
                if (student.File != null)
                {

                    string Filename = Guid.NewGuid().ToString() + Path.GetExtension(student.File.FileName);
                    string? studentpath = Path.Combine(Rootfile, @"images\students");

                    using (FileStream fileStream = new FileStream(Path.Combine(studentpath, Filename), FileMode.Create))
                    {
                        student.File.CopyTo(fileStream);
                    }

                    student.Image = @"/images/students/" + Filename;
                }



                _dbContext.Students.Add(student);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Departments = _dbContext.Departments.ToList();
                return View("Add", student);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = _dbContext.Students.FirstOrDefault(s => s.SSN == id);
            ViewBag.Departments = _dbContext.Departments.ToList();
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student, IFormFile? File)
        {
            if (student.Name != null && student.Email != null)
            {
                if (File != null)
                {
                    string Rootfile = webHostEnvironment.WebRootPath;
                    string Filename = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName);
                    string? studentpath = Path.Combine(Rootfile, @"images\students");

                    if (student.Image != null)
                    {
                        var OldImgPath = Path.Combine(Rootfile, student.Image.TrimStart('\\'));
                        if (System.IO.File.Exists(OldImgPath))
                        {
                            System.IO.File.Delete(OldImgPath);
                        }
                    }


                    using (FileStream fileStream = new FileStream(Path.Combine(studentpath, Filename), FileMode.Create))
                    {
                        File.CopyTo(fileStream);
                    }

                    student.Image = @"/images/students/" + Filename;
                }

                _dbContext.Update(student);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Departments = _dbContext.Departments.ToList();
                return View("Edit", student);
            }
        }


        public IActionResult ValidateAddress(string Address)
        {
            string[] validAddresses = { "Cairo", "Alexandria", "Ismailia" };
            bool isValid = validAddresses.Contains(Address);
            return Json(isValid);
        }



    }
}
