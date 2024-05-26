using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebIntroEmpty.Controllers;
using WebIntroEmpty.Models.Contexts;

namespace WebIntroEmpty
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseInMemoryDatabase("StudentsDb");
            });

            var app = builder.Build();
            app.UseStaticFiles();
            //app.MapDefaultControllerRoute();
            app.MapControllerRoute(name: "default", pattern: "{controller=students}/{action=index}/{id?}");

            app.Seed();

            app.Run();
        }
    }
}