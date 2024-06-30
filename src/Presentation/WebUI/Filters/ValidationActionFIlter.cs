using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace WebUI.Filters
{
    public class ValidationActionFilter : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
            if ("POST".Equals(context.HttpContext.Request.Method,StringComparison.OrdinalIgnoreCase) 
                && !context.ModelState.IsValid)
            {
                context.Result = new ViewResult
                {
                    ViewName = context.RouteData.Values["action"]?.ToString(),
                    ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), context.ModelState),
                };
            }
        }
    }
}
