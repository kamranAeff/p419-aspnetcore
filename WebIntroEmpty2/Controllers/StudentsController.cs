using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebIntroEmpty2.AppCode.LoggingConcept;
using WebIntroEmpty2.Models.Contexts;
using WebIntroEmpty2.Models.Entities;
using WebIntroEmpty2.Models.ViewModels;

namespace WebIntroEmpty2.Controllers
{
    public class StudentsController(DataContext db, IMyLogger logger) : Controller
    {

        public IActionResult Index()
        {
            try
            {
                ViewBag.Instance = logger.InstanceId;
                logger.WriteLog($"Students/Index");

                //var query = db.Students.Join(db.Groups,
                //     s => s.GroupId,
                //     g => g.Id,
                //     (s, g) => new StudentViewModel
                //     {
                //         Id = s.Id,
                //         Name = s.Name,
                //         Surname = s.Surname,
                //         GroupId = s.GroupId,
                //         GroupName = g.Name
                //     }
                //    );

                var query = from s in db.Students
                            join g in db.Groups on s.GroupId equals g.Id
                            select new StudentViewModel
                            {
                                Id = s.Id,
                                Name = s.Name,
                                Surname = s.Surname,
                                GroupId = s.GroupId,
                                GroupName = g.Name
                            };


                var students = query.ToList();

                return View(students);
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex.Message);
                ViewBag.Message = "Xeta bash verdi biraz sonra yeniden yoxlayin";
                return View();
            }
        }

        public IActionResult Create()
        {
            try
            {

                logger.WriteLog($"$[{Request.Method}]: Students/Create");

                var groups = db.Groups.ToList();

                ViewBag.Groups = new SelectList(groups, "Id", "Name");

                throw new ArgumentNullException();
                return View();
            }
            catch (Exception ex)
            {
                logger.WriteLog(ex.Message);
                ViewBag.Message = "Xeta bash verdi biraz sonra yeniden yoxlayin";
                return View();
            }
        }

        [HttpPost]
        public IActionResult Create(Student model)
        {
            logger.WriteLog($"$[{Request.Method}]: Students/Create");

            db.Students.Add(model);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int id)
        {
            logger.WriteLog($"$[{Request.Method}]: Students/Edit");

            var groups = db.Groups.ToList();

            ViewBag.Groups = new SelectList(groups, "Id", "Name");

            var student = db.Students.FirstOrDefault(m => m.Id == id);

            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(Student model)
        {
            logger.WriteLog($"$[{Request.Method}]: Students/Edit");

            db.Entry(model).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }


        //public IActionResult About()
        //{
        //    return View();
        //}
    }
}
