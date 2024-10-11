using System.Text.RegularExpressions;
using WebUI.Binders.LocalizationConcept;

namespace WebUI.Binders.ConstraintsConcept
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

            return Regex.IsMatch(language, LocalizeInjection.SUPPORTED_CULTURES, RegexOptions.IgnoreCase);
        }
    }
}
