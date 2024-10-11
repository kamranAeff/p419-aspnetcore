using System.Text.RegularExpressions;

namespace WebUI.Binders.LocalizationConcept
{
    class LocalizeRedirectionalMiddleware(RequestDelegate next)
    {
        public Task Invoke(HttpContext context)
        {
            bool hasLangCookie = context.Request.Cookies.TryGetValue("lang", out string lang);
            var match = Regex.Match(context.Request.Path, @$"\/(?<lang>{LocalizeInjection.SUPPORTED_CULTURES})\/?.*");

            if (!match.Success)
            {
                if (string.IsNullOrWhiteSpace(lang) || !Regex.IsMatch(lang, LocalizeInjection.SUPPORTED_CULTURES))
                    lang = "az";

                context.Response.Redirect($"/{lang}{context.Request.Path}{context.Request.QueryString}");
                return Task.CompletedTask;
            }
            else if (!hasLangCookie)
            {
                context.Response.Cookies.Append("lang", match.Groups["lang"].Value,
                    new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(3),
                        Path = "/",
                        HttpOnly = true
                    });
            }

            return next(context);
        }
    }
}
