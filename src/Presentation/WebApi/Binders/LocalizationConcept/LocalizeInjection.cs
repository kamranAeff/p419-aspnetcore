using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
using System.Text.RegularExpressions;

namespace WebApi.Binders.LocalizationConcept
{
    static class LocalizeInjection
    {
        public const string SUPPORTED_CULTURES = "az|en|ru";

        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {
            app.UseRequestLocalization(cfg =>
            {
                cfg.RequestCultureProviders.Clear();

                cfg.SupportedUICultures = cfg.SupportedCultures = SUPPORTED_CULTURES.Split('|').Select(m => new CultureInfo(m)).ToArray();
                cfg.RequestCultureProviders.Add(new LocalizeCultureProvider());
            });
            return app;
        }
    }
}