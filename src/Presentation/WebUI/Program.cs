using Domain.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence.Contexts;
using Services;
using System.Reflection;
using WebUI.Filters;

namespace WebUI
{
    public class Program
    {
        static string[] policies = null;

        public static void Main(string[] args)
        {
            LoadPolicies();

            var builder = WebApplication.CreateBuilder(args);
            Console.WriteLine(builder.Environment.EnvironmentName);

            builder.Host.UseServiceProviderFactory(new IoCFactory());

            builder.Services.AddControllersWithViews(cfg =>
            {
                cfg.Filters.Add(new GlobalExceptionFilter());
                cfg.Filters.Add(new ValidationActionFilter());

                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();

                cfg.Filters.Add(new AuthorizeFilter(policy));
            });

            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);

            builder.Services.AddDataContext(cfg =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"), opt =>
                {
                    opt.MigrationsHistoryTable("MigrationsHistory");
                });
            });

            builder.Services.Configure<EmailConfiguration>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));
            builder.Services.Configure<CryptoServiceConfiguration>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));

            //builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddHttpContextAccessor();

            builder.Services.AddFluentValidationAutoValidation(cfg =>
            {
                cfg.DisableDataAnnotationsValidation = true;
            });
            builder.Services.AddValidatorsFromAssemblyContaining<IServiceReference>(includeInternalTypes: true);


            builder.Services.Configure<IdentityOptions>(cfg =>
            {
                cfg.User.RequireUniqueEmail = true;
                //cfg.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";

                //cfg.Lockout.AllowedForNewUsers = true;
                cfg.Lockout.MaxFailedAccessAttempts = 3;
                cfg.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);

                if (builder.Environment.IsDevelopment())
                {
                    cfg.Password.RequiredUniqueChars = 1;
                    cfg.Password.RequiredLength = 3;
                    cfg.Password.RequireNonAlphanumeric = false;
                    cfg.Password.RequireUppercase = false;
                    cfg.Password.RequireLowercase = false;
                    cfg.Password.RequireDigit = false;
                }
            });

            builder.Services.AddAuthentication(cfg =>
            {
                cfg.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                cfg.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                cfg.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                cfg.DefaultSignOutScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                .AddCookie(cfg =>
            {
                cfg.LoginPath = "/signin.html";
                cfg.AccessDeniedPath = "/accessdenied.html";

                cfg.Cookie.Name = "ogani";
                cfg.Cookie.HttpOnly = true;
            });

            builder.Services.AddAuthorization(cfg =>
            {
                foreach (var item in policies)
                {
                    cfg.AddPolicy(item, opt =>
                    {
                        //opt.RequireClaim("admin.categories.create", "1");
                        opt.RequireAssertion(hendler =>
                        {
                            return hendler.User.IsInRole("SuperAdmin") || hendler.User.HasClaim(item, "1");
                        });
                    });
                }
            });

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

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
                pattern: "{controller=Home}/{action=index}/{id?}");
            #endregion

            app.Run();
        }

        private static void LoadPolicies()
        {
            var types = typeof(Program).Assembly.GetTypes();

            policies = types
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t) && t.IsDefined(typeof(AuthorizeAttribute), true))
                .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                .Union(
                types
                .Where(t => typeof(ControllerBase).IsAssignableFrom(t))
                .SelectMany(type => type.GetMethods())
                .Where(method => method.IsPublic
                 && !method.IsDefined(typeof(NonActionAttribute), true)
                 && method.IsDefined(typeof(AuthorizeAttribute), true))
                 .SelectMany(t => t.GetCustomAttributes<AuthorizeAttribute>())
                )
                .Where(a => !string.IsNullOrWhiteSpace(a.Policy))
                .SelectMany(a => a.Policy.Split(new[] { "," }, System.StringSplitOptions.RemoveEmptyEntries))
                .Distinct()
                .ToArray();
        }
    }
}
