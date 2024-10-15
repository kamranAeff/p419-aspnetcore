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
                case NotFoundException:
                    context.Result = new JsonResult(new
                    {
                        isSuccess = false,
                        code = StatusCodes.Status404NotFound,
                        message = "resurs not found"
                    });
                    break;
                case ForbiddenException:
                    context.Result = new JsonResult(new
                    {
                        isSuccess = false,
                        code = StatusCodes.Status403Forbidden,
                        message = "you havent permission"
                    });
                    break;
                case BadRequestException ex:
                    context.Result = new JsonResult(new
                    {
                        isSuccess = false,
                        code = StatusCodes.Status400BadRequest,
                        message = "please check your inputs",
                        errors = ex.Errors
                    });
                    break;
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
                case ForbiddenException:
                    context.Result = new ContentResult
                    {
                        Content = File.ReadAllText("wwwroot/error-pages/403.html"),
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