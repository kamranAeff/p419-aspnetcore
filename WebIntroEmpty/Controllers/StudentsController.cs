using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebIntroEmpty.Models.Contexts;
using WebIntroEmpty.Models.Entities;

namespace WebIntroEmpty.Controllers
{
    public class StudentsController : Controller
    {
        private readonly DataContext db;

        public StudentsController(DataContext db)
        {
            this.db = db;
        }

        public IActionResult Index()
        {
            var students = db.Students.ToList();

            return View(students);
        }

        public IActionResult Details(int id)
        {
            var student = db.Students.Where(m => m.Id == id).FirstOrDefault();

            return View(student);
        }

        public IActionResult Edit(int id)
        {
            var student = db.Students.Where(m => m.Id == id).FirstOrDefault();

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student model)
        {
            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student model)
        {
            db.Students.Add(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int id)
        {
            var student = db.Students.Where(m => m.Id == id).FirstOrDefault();


            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
