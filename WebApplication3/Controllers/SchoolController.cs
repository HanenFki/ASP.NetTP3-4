using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models.Repositories;
using WebApplication3.Models;
using WebApplication3.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication3.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class SchoolController : Controller

    {
        private readonly ISchoolRepository _schoolRepository;

        public SchoolController(ISchoolRepository schoolRepository)
        {
            _schoolRepository = schoolRepository;
        }

        [AllowAnonymous]
        // GET: SchoolController
        public ActionResult Index()
         {
            var schools = _schoolRepository.GetAll();
            return View(schools);
        }

        // GET: SchoolController/Details/5
        public ActionResult Details(int id)
        {
            var school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // GET: SchoolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SchoolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(School school)
        {
            try
            {
                _schoolRepository.Add(school);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }
            return View(school);
        }

        // GET: SchoolController/Edit/5
        public ActionResult Edit(int id)
        {
            var school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // POST: SchoolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, School school)
        {
            try
            {
                if (id != school.SchoolID)
                {
                    return NotFound();
                }


                _schoolRepository.Edit(school);
                return RedirectToAction(nameof(Index));

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, $"An error occurred: {ex.Message}");
            }
            return View(school);
        }

        // GET: SchoolController/Delete/5
        public ActionResult Delete(int id)
        {
            School school = _schoolRepository.GetById(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }



        // POST: SchoolController/Delete/5
        [HttpPost, ActionName("Delete")]

        public ActionResult Delete(School s)
        {
            _schoolRepository.Delete(s);
            return RedirectToAction(nameof(Index));
        }

    }

}

