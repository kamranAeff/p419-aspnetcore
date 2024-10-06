using Microsoft.OpenApi.Models;
using WebApi.Swagger.OperationFilters;

namespace WebApi.Swagger
{
    public static class SwaggerInjection
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.DescribeAllParametersInCamelCase();

                cfg.OperationFilter<RefreshTokenHeaderOperationFilter>();
                cfg.OperationFilter<ResponseStatusOperationFilter>();
                cfg.OperationFilter<LanguageHeaderOperationFilter>();
                cfg.EnableAnnotations();

                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer"
                });

                cfg.AddSecurityRequirement(new OpenApiSecurityRequirement
                                        {
                                            {
                                                new OpenApiSecurityScheme
                                                {
                                                    Reference = new OpenApiReference
                                                    {
                                                        Type = ReferenceType.SecurityScheme,
                                                        Id = "Bearer"
                                                    }
                                                },
                                                Array.Empty<string>()
                                            }
                                        });
            });

            return services;
        }


        public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
        {
            SwaggerBuilderExtensions.UseSwagger(app);
            SwaggerUIBuilderExtensions.UseSwaggerUI(app);

            return app;
        }
    }
}
