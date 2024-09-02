using Application;
using Application.Behaviors;
using Domain.Configurations;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Persistence.Contexts;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using WebApi.Binders.BooleanConcept;
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
            LoadPolicies();

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
                cfg.ModelBinderProviders.Insert(0, new EnumerableModelBinderProvider());
                cfg.ModelBinderProviders.Insert(0, new BooleanBinderProvider());

                var policy = new AuthorizationPolicyBuilder()
                                  .RequireAuthenticatedUser()
                                  .Build();

                cfg.Filters.Add(new AuthorizeFilter(policy));

            })
                .AddJsonOptions(cfg =>
                {
                    cfg.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                    cfg.JsonSerializerOptions.Converters.Add(new EnumerableConverter<int>());
                    cfg.JsonSerializerOptions.Converters.Add(new EnumerableConverter<string>());
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
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(PageableBinderPipelineBehavior<,>));
                //cfg.AddBehavior(typeof(IPipelineBehavior<,>),typeof(PerformanceBehavior<,>));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            });

            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(cfg => cfg.AddPolicy("allowAll", p => p.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

            builder.Services.AddAuthentication(cfg =>
            {
                cfg.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                cfg.DefaultSignOutScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, cfg =>
            {

                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Environment.GetEnvironmentVariable("JWT__ISSUER")!,
                    ValidAudience = Environment.GetEnvironmentVariable("JWT__AUDIENCE")!,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT__KEY")!)),
                    ClockSkew = TimeSpan.Zero,
                    LifetimeValidator = (notBefore, expires, tokenToValidate, @param) => expires != null && expires > DateTime.UtcNow
                };
            });

            builder.Services.AddAuthorization(cfg =>
            {
                foreach (var policy in ClaimsTransformation.policies)
                {
                    cfg.AddPolicy(policy, opt => opt.RequireAssertion(handler => handler.User.IsInRole("SUPERADMIN") || handler.User.HasClaim(m => m.Type.Equals(policy) && m.Value.Equals("1"))));
                }
            });

            var app = builder.Build();

            app.UseAuthentication();
            app.UseAuthorization();

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
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.WebRootPath,"uploads"))
            });

            app.MapControllers();

            app.Run();
        }

        private static void LoadPolicies()
        {
            var types = typeof(Program).Assembly.GetTypes();

            ClaimsTransformation.policies = types
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
