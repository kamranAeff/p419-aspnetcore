using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebUI.Filters
{
    public class ValidationActionFilter : IActionFilter
    {

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var result = new ViewResult
                {
                    ViewName = context.RouteData.Values["action"]?.ToString(),
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState),
                };

                context.Result = result;
            }

            Console.WriteLine($"ValidationActionFilterStart {DateTime.Now:HH:mm:ss.ffffff}");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            Console.WriteLine($"ValidationActionFilterEnd {DateTime.Now:HH:mm:ss.ffffff}");
        }
    }
}
