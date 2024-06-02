using Coravel;
using Microsoft.EntityFrameworkCore;
using ScrappingService.Invoceables;
using ScrappingService.Models;

namespace ScrappingService
{

    //https://crontab.guru/#*_*_*_*_*
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddScheduler();
            builder.Services.AddDbContext<DataContext>(cfg =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"));
            },contextLifetime:ServiceLifetime.Scoped);

            //builder.Services.AddHostedService<Worker>();
            builder.Services.AddScoped<CurrencyInvoceable>();
            builder.Services.AddScoped<CheckWeatherInvoceable>();


            var host = builder.Build();

            host.Services.UseScheduler(scheduler =>
            {

                scheduler.Schedule<CurrencyInvoceable>()
                         .EverySeconds(1)
                         .PreventOverlapping(typeof(CurrencyInvoceable).Name);
                //.Cron("* * * * *");

                scheduler.Schedule<CheckWeatherInvoceable>()
                         .EverySeconds(1)
                         .PreventOverlapping(typeof(CheckWeatherInvoceable).Name);

            });

            host.Run();
        }
    }
}