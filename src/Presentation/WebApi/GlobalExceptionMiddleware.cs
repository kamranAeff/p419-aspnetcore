using Domain.Exceptions;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebApi.Models.Common;

namespace WebApi
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<GlobalExceptionMiddleware> logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                var response = ex switch
                {
                    UserNameOrPasswordIncorrectException or
                    AccountLockoutException or
                    UnverifiedEmailException or
                    UnverifiedPhoneException => ApiResponse.Fail(StatusCodes.Status401Unauthorized, ex.Message),

                    NotFoundException nfEx => ApiResponse.Fail(StatusCodes.Status404NotFound, nfEx.Message),
                    BadRequestException brEx => ApiResponse.Fail(StatusCodes.Status400BadRequest, brEx.Errors, brEx.Message),
                    UnauthorizedException => ApiResponse.Fail(StatusCodes.Status401Unauthorized, ex.Message),
                    _ => ApiResponse.Fail(StatusCodes.Status500InternalServerError, "ServerError")
                };

                context.Response.StatusCode = response.Code;

                logger.LogError(ex, "Error Handled: StatusCode=@Code, Errors=@Errors", response.Code, response.Errors);

                await context.Response.WriteAsJsonAsync(response, options: new JsonSerializerOptions
                {
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
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
