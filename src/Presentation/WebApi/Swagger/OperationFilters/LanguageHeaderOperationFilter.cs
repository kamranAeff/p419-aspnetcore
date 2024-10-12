using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Interfaces;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Swagger.OperationFilters
{
    public class LanguageHeaderOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Parameters == null)
                operation.Parameters = new List<OpenApiParameter>();

            var languageParameter = new OpenApiParameter
            {
                Name = "Accept-Language",
                Description = "Language of Interface",
                AllowEmptyValue = false,
                Required = true,
                In = ParameterLocation.Header,
                Schema = new OpenApiSchema
                {
                    Type = "string",
                    Title = "Language",
                    Extensions = new Dictionary<string, IOpenApiExtension>
                    {
                        ["x-ms-enum"] = new OpenApiString("Accept-Language"),
                        ["enum"] = new OpenApiArray
                                {
                                    new OpenApiString("az"),
                                    new OpenApiString("en"),
                                    new OpenApiString("ru")
                                }
                    }
                }
            };

            operation.Parameters.Add(languageParameter);
        }
    }
}
