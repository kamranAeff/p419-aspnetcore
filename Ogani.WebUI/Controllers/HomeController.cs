using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ogani.WebUI.AppCode.Services;
using Ogani.WebUI.Models.Contexts;
using Ogani.WebUI.Models.Entities;
using System.Web;
using static System.Net.Mime.MediaTypeNames;

namespace Ogani.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly DataContext db;
        private readonly IEmailService emailService;
        private readonly ICryptoService cryptoService;

        public HomeController(DataContext db, IEmailService emailService, ICryptoService cryptoService)
        {
            this.db = db;
            this.emailService = emailService;
            this.cryptoService = cryptoService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(string fullName, string email, string message)
        {
            var post = new ContactPost
            {
                FullName = fullName,
                Email = email,
                Message = message,
                CreatedAt = DateTime.Now
            };

            db.ContactPosts.Add(post);
            db.SaveChanges();

            return Json(new
            {
                error = false,
                message = "Muracietiniz qebul edildi.2 is gunu erzinde size geri doneceyik!"
            });
        }

        [HttpPost]
        public async Task<IActionResult> Subscribe(string email)
        {
            var entity = await db.Subscribers.FirstOrDefaultAsync(x => x.Email.Equals(email));

            if (entity?.ApprovedAt is not null)
            {
                return Json(new
                {
                    error = true,
                    message = "Siz artıq abunə olmusunuz!"
                });
            }
            else if (entity is not null)
            {
                return Json(new
                {
                    error = true,
                    message = "Siz abunəlik üçün e-poçt unvanınızı təsdiq etməlisiniz!"
                });
            }


            entity = new Subscribe { Email = email, CreatedAt = DateTime.Now };
            await db.Subscribers.AddAsync(entity);
            await db.SaveChangesAsync();

#warning email-e tesdiq ucun link gondermeliydik
            string token = $"id={entity.Email}+expire={DateTime.Now.AddHours(1):yyyy.MM.dd HH:mm:ss}";

            token = cryptoService.Encrypt(token, true);


            string redirectUrl = $"http://localhost:5178/subscribe-approve?token={token}";

            string msg = $"Salam <b>Aqil Abbasov</b>,<br/>Abuneliyinizi <a href=\"{redirectUrl}\">link</a>`lə tamamlayin";

            await emailService.SendEmail(entity.Email, "Ogani Subscription", msg);


            return Json(new
            {
                error = false,
                message = $"{email} sorgunu aldiq"
            });
        }

        [Route("/subscribe-approve")]
        public IActionResult S(string token)
        {
            token = cryptoService.Decrypt(token);
            return Content(token);
        }


        public IActionResult Encrypt(string text)
        {
            ViewBag.Text = text;
            ViewBag.CipherText = cryptoService.Encrypt(text);
            return View("Crypto");
        }

        public IActionResult Decrypt(string text)
        {
            ViewBag.CipherText1 = text;
            ViewBag.Text1 = cryptoService.Decrypt(text);
            return View("Crypto");
        }
    }
}
