using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.Repositories;
using WebApplication3.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using WebApplication3.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class StudentController : Controller
    {
        private readonly IStudentRepository studentRepository;
        private readonly ISchoolRepository schoolRepository;

        public StudentController(IStudentRepository studentRepository, ISchoolRepository schoolRepository)
        {
            this.studentRepository = studentRepository;
            this.schoolRepository = schoolRepository;
        }
        // GET: School/Details/5
        [AllowAnonymous]
        public IActionResult Details(int id)
        {


            var student = studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: StudentController
        public IActionResult Index()
        {
            var students = studentRepository.GetAll();
            return View(students);
        }

        // GET: StudentController/Create
        public IActionResult Create()
        {
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View();
        }

        // POST: StudentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Student student)
        {
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            studentRepository.Add(student);
            return RedirectToAction(nameof(Index));
        }

        // GET: StudentController/Edit/5
        public IActionResult Edit(int id)
        {
            var student = studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName", student.SchoolID);
            return View(student);
        }

        // POST: StudentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Student student)
        {
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName", student.SchoolID);
            try
            {
                studentRepository.Edit(student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(student.StudentId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: StudentController/Delete/5
        public IActionResult Delete(int id)
        {
            var student = studentRepository.GetById(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: StudentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Student student)
        {
            studentRepository.Delete(student);
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return studentRepository.GetById(id) != null;
        }



        public ActionResult Search(string name, int? schoolid)

        {
            var result = studentRepository.GetAll();
            if (!string.IsNullOrEmpty(name))
                result = studentRepository.FindByName(name);
            else
            if (schoolid != null)
                result = studentRepository.GetStudentsBySchoolID(schoolid);
            ViewBag.SchoolID = new SelectList(schoolRepository.GetAll(), "SchoolID", "SchoolName");
            return View("Index", result);
        }



    }
}
