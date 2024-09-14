using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using WebUI.Exceptions;

namespace WebUI.Filters
{
    public class GlobalExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;

            Console.WriteLine(context.Exception.Message);

            if ("XMLHttpRequest".Equals(context.HttpContext.Request.Headers["X-Requested-With"]))
            {
                GenerateAjaxResponse(context);
                return;
            }

            GenerateHttpResponse(context);
        }

        private void GenerateAjaxResponse(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case NullReferenceException:
                case ArgumentNullException:
                default:
                    context.Result = new JsonResult(new
                    {
                        error = true,
                        message = "nese o deyil"
                    });
                    break;
            }
        }

        private void GenerateHttpResponse(ExceptionContext context)
        {
            switch (context.Exception)
            {
                case UnauthorizedAccessException:
                    context.Result = new RedirectResult("~/signin.html");
                    break;
                case BadRequestException ex:
                    context.ModelState.Clear();
                    foreach (var itemError in ex.Errors)
                    {
                        foreach (var message in itemError.Value)
                            context.ModelState.AddModelError(itemError.Key, message);
                    }

                    context.Result = new ViewResult
                    {
                        ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState),
                        ViewName = context.RouteData.Values["action"]?.ToString()
                    };
                    break;
                case NotFoundException:
                    context.Result = new ContentResult
                    {
                        Content = File.ReadAllText("wwwroot/error-pages/404.html"),
                        ContentType = "text/html",
                        StatusCode = 200
                    };
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
