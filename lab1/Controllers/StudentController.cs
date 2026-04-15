using lab1.Interfaces.IRepositories;
using lab1.Models;
using lab1.ViewModels.StudentVM;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab1.Controllers
{
    public class StudentController : Controller
    {

        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IStudentRepo studentRepo;
        private readonly IDepartmentRepo departmentRepo;

        public StudentController(IWebHostEnvironment webHostEnvironment, IStudentRepo studentRepo
            , IDepartmentRepo departmentRepo)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.studentRepo = studentRepo;
            this.departmentRepo = departmentRepo;
        }
        public IActionResult Index()
        {
            var students = studentRepo.GetAll();
            return View("Index", students);
        }

        public IActionResult Details(int id)
        {
            var student = studentRepo.GetStudentWithFullDetails(id);

            var vm = student.Adapt<GetStudentWithFullDetialsVM>();
            return View("Details", vm);
        }

        public IActionResult Add()
        {
            ViewBag.Departments = departmentRepo.GetAll().ToList();
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



                studentRepo.Add(student);
                studentRepo.Save();
                TempData["Success"] = "Student added successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Departments = departmentRepo.GetAll().ToList();
                return View("Add", student);
            }
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student = studentRepo.GetById(id);
            ViewBag.Departments = departmentRepo.GetAll().ToList();
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student student, IFormFile? File)
        {
            if (ModelState.IsValid)
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

                studentRepo.Update(student);
                studentRepo.Save();
                TempData["Success"] = "Student updated successfully!";
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Departments = departmentRepo.GetAll().ToList();
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
