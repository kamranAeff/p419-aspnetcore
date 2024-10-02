using System.Text.RegularExpressions;

namespace WebApi.Binders.ConstraintsConcept
{
    public class LanguageRouteConstraint : IRouteConstraint
    {
        public bool Match(HttpContext? httpContext, IRouter? route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (!values.ContainsKey(routeKey))
                return false;

            var language = values[routeKey]?.ToString();

            if (string.IsNullOrEmpty(language) || long.TryParse(language, out long temp))
                return false;

            return Regex.IsMatch(language, @"en|az|ru", RegexOptions.IgnoreCase);
        }
    }
}
