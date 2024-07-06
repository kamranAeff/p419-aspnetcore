using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace WebUI.Controllers
{

    public class SelectedChooseItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Selected { get; set; }
    }

    public class LessonsController : Controller
    {
        public string Index()
        {
            var time = Request.HttpContext.Session.GetString("time");

            if (time is null)
            {
                time = $"{DateTime.Now:yyyy.MM.dd HH:mm:ss.fff}";

                Request.HttpContext.Session.SetString("time", time);
            }

            return time;
        }

        public IActionResult CheckCookie()
        {
            var fruitIds = Request.Cookies["fruitIds"]
                ?.Split(",", StringSplitOptions.RemoveEmptyEntries)
                .Select(m => int.TryParse(m, out int v) ? v : 0)
                ?? Array.Empty<int>();

            var data = new[] {
                new { Id = 1,Name="Alma"},
                new { Id = 2,Name="Heyva"},
                new { Id = 3,Name="Nar"},
                new { Id = 4,Name="Armud"},
            };

            //query base linq
            var result = from d in data
                         join v in fruitIds on d.Id equals v into resultSet
                         from j in resultSet.DefaultIfEmpty()
                         select new SelectedChooseItem
                         {
                             Id = d.Id,
                             Name = d.Name,
                             Selected = j != 0
                         };


            Response.Cookies.Append("author", "P419", new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7),
            });
            return View(result);

            ////method base linq
            //var result = data
            //                 .GroupJoin(
            //                     fruitIds,
            //                     l => l.Id,
            //                     r => r,
            //                     (l, rGroup) => new { LeftItem = l, RightItems = rGroup.DefaultIfEmpty() }
            //                 )
            //                 .SelectMany(
            //                     x => x.RightItems,
            //                     (parent, right) => new SelectedChooseItem
            //                     {
            //                         Id = parent.LeftItem.Id,
            //                         Name = parent.LeftItem.Name,
            //                         Selected = right != 0
            //                     }
            //                 );
        }
    }
}
