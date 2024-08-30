using Domain.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Models.Common;

namespace WebApi
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalExceptionMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                ApiResponse response = null;
                switch (ex)
                {
                    case NotFoundException nfEx:
                        response = ApiResponse.Fail(StatusCodes.Status404NotFound, nfEx.Message);
                        break;
                    case BadRequestException brEx:
                        response = ApiResponse.Fail(StatusCodes.Status400BadRequest, brEx.Errors, brEx.Message);
                        break;
                    default:
                        response = ApiResponse.Fail(StatusCodes.Status500InternalServerError, "ServerError");
                        break;
                }

                context.Response.StatusCode = response.Code;
                await context.Response.WriteAsJsonAsync(response, options: new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

                //context.Result = new JsonResult(response)
                //{
                //    StatusCode = response.Code
                //};
            }

        }
    }


    public static class GlobalExceptionMiddlewareExtension
    {
        public static IApplicationBuilder UseGlobalException(this IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionMiddleware>();

            return app;
        }
    }
}
