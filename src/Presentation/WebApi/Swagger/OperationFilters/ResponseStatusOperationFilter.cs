using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApi.Swagger.OperationFilters
{
    public class ResponseStatusOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            operation.Responses.Add("400", new OpenApiResponse { Description = "Bad Request" });

            if (context.ApiDescription.HttpMethod != "POST")
                operation.Responses.Add("404", new OpenApiResponse { Description = "Not Found" });
            else if ("POST".Equals(context.ApiDescription.HttpMethod))
                operation.Responses.Add("201", new OpenApiResponse { Description = "Created" });


            var authorizeAttributes = context.MethodInfo.GetCustomAttributes(true).OfType<AuthorizeAttribute>();

            if (authorizeAttributes.Any())
            {
                operation.Responses.Add("401", new OpenApiResponse { Description = "Unauthorized" });
                var policies = authorizeAttributes.Where(m => !string.IsNullOrWhiteSpace(m.Policy))
                    .Select(attr => attr.Policy).Distinct().ToArray();

                if (policies.Length > 0)
                {
                    operation.Responses.Add("403", new OpenApiResponse { Description = "Forbidden" });
                    operation.Description += $"<b>Required Polic{(policies.Length > 1 ? "ies" : "y")}:</b> <u>{string.Join(", ", policies)}</u>";
                }
            }
        }
    }
}
