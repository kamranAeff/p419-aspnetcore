using Microsoft.AspNetCore.Localization;
using System.Text.RegularExpressions;

namespace WebUI.Localization.Implementation
{
    public class AppCultureProvider : RequestCultureProvider
    {
        async public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext httpContext)
        {
            if (httpContext == null)
                throw new ArgumentNullException(nameof(httpContext));

            string lang = "az";

            var match = Regex.Match(httpContext.Request.Path, @"\/(?<lang>en|az|ru)\/?.*");

            if (match.Success && !string.IsNullOrWhiteSpace(match.Groups["lang"].Value))
            {
                httpContext.Response.Cookies.Delete("lang");
                httpContext.Response.Cookies.Append("lang", match.Groups["lang"].Value,
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(3)
                    });

                return new ProviderCultureResult(match.Groups["lang"].Value, match.Groups["lang"].Value);
            }

            httpContext.Request.Cookies.TryGetValue("lang", out lang);
            if (string.IsNullOrWhiteSpace(lang))
                lang = "az";

            var newUrl = $"{lang}{httpContext.Request.Path}";
            httpContext.Request.RouteValues.Clear();
            httpContext.Request.RouteValues.Add("lang",lang);
            httpContext.Response.Redirect(newUrl, true);
           await httpContext.Response.CompleteAsync();
            return null;
        }
    }
}
