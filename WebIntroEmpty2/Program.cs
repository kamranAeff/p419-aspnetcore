using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebIntroEmpty2.AppCode.LoggingConcept;
using WebIntroEmpty2.AppCode.Middlewares;
using WebIntroEmpty2.Models.Contexts;

namespace WebIntroEmpty2
{
    public class Program
    {
        public static void Main(string[] args)
        {

            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"));
            }, optionsLifetime: ServiceLifetime.Scoped);

            //builder.Services.AddSingleton<IMyLogger,DbLogger>();
            //builder.Services.AddScoped<IMyLogger,DbLogger>();
            builder.Services.AddTransient<IMyLogger, DbLogger>();

            var app = builder.Build();

            //app.UseMiddleware<StopwatchMiddleware>();
            app.UseStopwatch();
            #region BuiltIn middlewares
            //app.Use(async (context, next) =>
            //{
            //    Stopwatch stopwatch = new Stopwatch();
            //    stopwatch.Start();

            //    context.Response.OnStarting((object ctx) => {

            //        stopwatch.Stop();

            //        if (ctx is HttpContext c)
            //        {
            //            c.Response.Headers.Add("Elapsed", $"{stopwatch.ElapsedMilliseconds}ms");
            //        }

            //        return Task.CompletedTask;

            //    },context);


            //    await next();
            //});


            //app.Run(async (context) =>
            //{
            //    Stopwatch stopwatch = new Stopwatch();
            //    stopwatch.Start();

            //    context.Response.OnStarting((object ctx) => {

            //        stopwatch.Stop();

            //        if (ctx is HttpContext c)
            //        {
            //            c.Response.Headers.Add("Elapsed", $"{stopwatch.ElapsedMilliseconds}ms");
            //        }

            //        return Task.CompletedTask;

            //    }, context);

            //    await context.Response.WriteAsync("Salam dostlar");
            //});
            #endregion

            app.UseStaticFiles();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=students}/{action=index}/{id?}");



            app.Run();
        }
    }
}
