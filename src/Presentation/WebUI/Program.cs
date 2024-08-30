using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebUI.Services.BlogPost;
using WebUI.Services.Brands;
using WebUI.Services.Categories;
using WebUI.Services.Products;
using WebUI.Services.Tags;

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
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<ICategoryService, CategoryService>();
            builder.Services.AddSingleton<IBrandService, BrandService>();
            builder.Services.AddSingleton<ITagService, TagService>();

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

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
                pattern: "{controller=blog}/{action=index}/{id?}");
            #endregion

            app.Run();
        }


    }
}
