
using System.Text.RegularExpressions;

namespace WebApi.Binders.ConstraintsConcept
{
    public class SlugRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext,
            IRouter? route,
            string routeKey,
            RouteValueDictionary values,
            RouteDirection routeDirection)
        {

            if (!values.ContainsKey(routeKey))
                return false;


            var slug = values[routeKey]?.ToString();

            if (string.IsNullOrEmpty(slug))
                return false;

            return Regex.IsMatch(slug, @"[a-z0-9-]+", RegexOptions.IgnoreCase);
        }
    }
}
