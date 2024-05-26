using Microsoft.AspNetCore.Mvc;
using WebIntroEmpty2.Models.Contexts;
using WebIntroEmpty2.Models.Entities;

namespace WebIntroEmpty2.Controllers
{
    public class StudentsController(DataContext db) : Controller
    {

        public IActionResult Index()
        {
            var group = new Group
            {
                Id = 1,
                Name = "P419"
            };

            db.Groups.Add(group);
            db.SaveChanges();

            var student = new Student { Id = 1, Name = "Aqil", Surname = "Abbasov", GroupId = 1 };
            db.Students.Add(student);
            db.SaveChanges();

            var students = db.Students.ToList();

            return View(students);
        }
    }
}
