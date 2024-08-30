using Application;
using Application.Behaviors;
using Domain.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Persistence.Contexts;
using System.Reflection;
using System.Text.Json.Serialization;
using WebApi.Binders.ConstraintsConcept;
using WebApi.Binders.EnumerableConcept;
using WebApi.MapperConfiguration.BlogPosts;
using WebApi.Middlewares;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Host.UseServiceProviderFactory(new IoCFactory());
            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<BlogPostProfile>();
            });

            builder.Services.InjectConstraints();
            builder.Services.AddControllers(cfg =>
            {
                //cfg.Filters.Add(new GlobalExceptionFilter());
                //cfg.ModelBinderProviders.Insert(0,new EnumerableModelBinderProvider());
            })
                .AddJsonOptions(cfg =>
                {
                    cfg.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    //cfg.JsonSerializerOptions.Converters.Add(new EnumerableConverter<int>());
                    //cfg.JsonSerializerOptions.Converters.Add(new EnumerableConverter<string>());
                    cfg.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddDataContext(cfg =>
            {
                cfg.UseSqlServer(builder.Configuration.GetConnectionString("cString"), opt =>
                {
                    opt.MigrationsHistoryTable("MigrationsHistory");
                });
            });

            builder.Services.AddRouting(cfg => cfg.LowercaseUrls = true);

            builder.Services.Configure<EmailConfiguration>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));
            builder.Services.Configure<CryptoServiceConfiguration>(cfg => builder.Configuration.GetSection(cfg.GetType().Name).Bind(cfg));

            builder.Services.AddHttpContextAccessor();

            builder.Services.AddFluentValidationAutoValidation(cfg =>
            {
                cfg.DisableDataAnnotationsValidation = true;
            });
            builder.Services.AddValidatorsFromAssemblyContaining<IApplicationReference>(includeInternalTypes: true);
            builder.Services.AddScoped<IValidatorInterceptor, ValidatorInterceptor>();

            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblyContaining<IApplicationReference>();
                cfg.AddBehavior(typeof(IPipelineBehavior<,>),typeof(PageableBinderPipelineBehavior<,>));
                //cfg.AddBehavior(typeof(IPipelineBehavior<,>),typeof(PerformanceBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            });

            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(cfg => cfg.AddPolicy("allowAll", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            var app = builder.Build();
            app.AutoMigration(); //applied auto migration

            app.UseDbTransaction();

            app.UseCors("allowAll");
            app.UseGlobalException();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/files",
                FileProvider = new PhysicalFileProvider(builder.Configuration["FilePath"]!)
            });

            app.MapControllers();

            app.Run();
        }

        private static void LoadPolicies()
        {
            var types = typeof(Program).Assembly.GetTypes();

            var policies = types
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
