using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebUI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
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
