using System.Globalization;

namespace WebUI.Localization.Implementation
{
    public static class LocalizeInjection
    {
        internal const string SUPPORTED_CULTURES = "az|en|ru";

        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {
            app.UseMiddleware<LocalizeRedirectionalMiddleware>();

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
