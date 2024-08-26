using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using System.Text;

namespace WebUI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();


            var sb = new StringBuilder();

            sb.AppendLine($"ASPNETCORE_ENVIRONMENT: {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}");
            sb.AppendLine($"REDIS_HOST: {Environment.GetEnvironmentVariable("REDIS_HOST")}");
            sb.AppendLine($"REDIS_USER: {Environment.GetEnvironmentVariable("REDIS_USER")}");

            Console.WriteLine(sb.ToString());

            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);

            builder.Services.AddDbContext<DbContext, DataContext>(cfg =>
            {

                cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"), opt =>
                {
                    opt.MigrationsHistoryTable("MigrationsHistory");
                });

            });


            //builder.Services.Configure<EmailConfiguration>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));
            //builder.Services.Configure<CryptoServiceConfiguration>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));

            //builder.Services.AddSingleton<IEmailService, EmailService>();
            //builder.Services.AddSingleton<ICryptoService, CryptoService>();
            ////builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            //builder.Services.AddHttpContextAccessor();


            //builder.Services.AddScoped<IContactPostService, ContactPostService>();
            //builder.Services.AddScoped<ISubscribeService, SubscribeService>();

            var app = builder.Build();
            app.UseStaticFiles();

            app.MapControllerRoute(name: "areas",
                pattern: "{area:exists}/{controller=Dashboard}/{action=index}/{id?}");

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=home}/{action=index}/{id?}");

            app.Run();
        }
    }
}
