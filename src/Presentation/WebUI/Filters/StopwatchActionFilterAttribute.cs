using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUI.Filters
{
    public class TestActionFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //if (!context.ModelState.IsValid)
            //{
            //    context.Result = new JsonResult(new
            //    {
            //        error = true,
            //        description = "validation error"
            //    });
            //}

            Console.WriteLine($"TestActionFilterStart {DateTime.Now:HH:mm:ss.ffffff}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"TestActionFilterEnd {DateTime.Now:HH:mm:ss.ffffff}");
        }
    }

    public class TestAuthFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (context.HttpContext.User?.Identity?.IsAuthenticated == true)
            {
                Console.WriteLine($"TestAuthFilter # Logged in {DateTime.Now:HH:mm:ss.ffffff}");
            }
            else
            {
                Console.WriteLine($"TestAuthFilter # LoginNotFound {DateTime.Now:HH:mm:ss.ffffff}");
            }
        }
    }

    public class TestResultFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext context)
        {
            Console.WriteLine($"TestResultFilterStart {DateTime.Now:HH:mm:ss.ffffff}");
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            Console.WriteLine($"TestResultFilterEnd {DateTime.Now:HH:mm:ss.ffffff}");

        }
    }

    public class TesExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            context.Result = new ContentResult()
            {
                Content = "biraz sonra yeniden yoxlayin",
                ContentType = "text/plain",
                StatusCode = 200
            };
            Console.WriteLine($"TesExceptionFilter {DateTime.Now:HH:mm:ss.ffffff}\n{context.Exception.Message}");
        }
    }
}
