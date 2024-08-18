using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebUI.Services.BlogPost;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllersWithViews();

            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);

            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddHttpClient("httpClient");

            builder.Services.AddSingleton<IBlogPostService, BlogPostService>();

            var app = builder.Build();
            app.UseStaticFiles();

            #region Routing
            app.MapGet("/accessdenied.html", async (context) =>
            {
                context.Response.Clear();
                context.Response.ContentType = "text/html";
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(File.ReadAllText("wwwroot/error-pages/403.html"));
            });

            app.MapControllerRoute(name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Blog}/{action=index}/{id?}");
            #endregion

            app.Run();
        }

        
    }
}
