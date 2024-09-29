using System.Globalization;

namespace WebUI.Localization.Implementation
{
    public static class LocalizeInjection
    {
        public static IApplicationBuilder UseLocalization(this IApplicationBuilder app)
        {
            app.UseRequestLocalization(cfg =>
            {
                var supportedCultures = new[] {
                   new CultureInfo("en"),
                   new CultureInfo("az"),
                   new CultureInfo("ru")
                };

                cfg.SupportedCultures = supportedCultures;
                cfg.SupportedUICultures = supportedCultures;

                cfg.RequestCultureProviders.Clear(); 
                cfg.RequestCultureProviders.Add(new AppCultureProvider());
            });
            return app;
        }
    }
}
