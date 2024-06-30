using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            switch (context.Exception)
            {
                case NullReferenceException:
                case ArgumentNullException:
                    context.Result = new ContentResult
                    {
                        Content = File.ReadAllText("wwwroot/error-pages/404.html"),
                        ContentType = "text/html",
                        StatusCode = 200
                    };
                    //context.Result = new NotFoundResult();
                    //Console.WriteLine($"{aEx.ParamName}: {aEx.Message}");
                    //context.Result = new NotFoundObjectResult(new { error = 404 });
                    //context.Result = new StatusCodeResult(StatusCodes.Status404NotFound);
                    break;
                default:
                    context.Result = new ContentResult
                    {
                        Content = File.ReadAllText("wwwroot/error-pages/500.html"),
                        ContentType = "text/html",
                        StatusCode = 200
                    };
                    break;
            }
        }
    }
}
