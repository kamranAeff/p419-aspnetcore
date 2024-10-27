using Microsoft.AspNetCore.Mvc.Infrastructure;
using WebUI.Binders.BooleanConcept;
using WebUI.Binders.ConstraintsConcept;
using WebUI.Binders.LocalizationConcept;
using WebUI.Filters;
using WebUI.Services.Account;
using WebUI.Services.BlogPost;
using WebUI.Services.Brands;
using WebUI.Services.Categories;
using WebUI.Services.Colors;
using WebUI.Services.Common;
using WebUI.Services.ContactPosts;
using WebUI.Services.Products;
using WebUI.Services.Sizes;
using WebUI.Services.Tags;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews(cfg =>
            {

                cfg.Filters.Add<GlobalExceptionFilter>(); 
                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());

            });

            builder.Services.InjectConstraints();
            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);

            builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            builder.Services.AddScoped<ProxyAuthorizationMessageHandler>();
            builder.Services.AddScoped<ProxyExceptionMessageHandler>();

            builder.Services.AddHttpClient("httpClient")
                .AddHttpMessageHandler<ProxyExceptionMessageHandler>()
                .AddHttpMessageHandler<ProxyAuthorizationMessageHandler>();

            builder.Services.AddSingleton<IContactPostsService, ContactPostsService>();
            builder.Services.AddSingleton<IAccountService, AccountService>();
            builder.Services.AddSingleton<IBlogPostService, BlogPostService>();
            builder.Services.AddSingleton<IProductService, ProductService>();
            builder.Services.AddSingleton<ICategoryService, CategoryService>();
            builder.Services.AddSingleton<IBrandService, BrandService>();
            builder.Services.AddSingleton<IColorService, ColorService>();
            builder.Services.AddSingleton<ISizeService, SizeService>();
            builder.Services.AddSingleton<ITagService, TagService>();

            builder.Services.AddAutoMapper(typeof(Program).Assembly);

            var app = builder.Build();
            app.UseStaticFiles();
            app.UseLocalization();

            #region Routing

            app.MapControllerRoute(
                name: "default",
                pattern: "{lang:language}/{controller=blog}/{action=index}/{id?}");

            app.MapControllerRoute(name: "areas",
                pattern: "{lang:language}/{area:exists}/{controller=Dashboard}/{action=index}/{id?}");

            app.MapControllerRoute(name: "blog",
                pattern: "{lang:language}/blog/{slug:slug}",
                defaults: new { controller = "Blog", action = "Details", area = "" });

            app.MapControllerRoute(name: "shop",
                pattern: "{lang:language}/shop/{slug:slug}",
                defaults: new { controller = "Shop", action = "Details", area = "" });

            app.MapGet("{lang:language}/accessdenied.html", async (context) =>
            {
                context.Response.Clear();
                context.Response.ContentType = "text/html";
                context.Response.StatusCode = 200;
                await context.Response.WriteAsync(File.ReadAllText("wwwroot/error-pages/403.html"));
            });
            #endregion

            app.Run();
        }
    }


    
}
