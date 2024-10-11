using Microsoft.AspNetCore.Localization;

namespace WebApi.Binders.LocalizationConcept
{
    class LocalizeCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext context)
        {
            string lang = context.Request.Headers.AcceptLanguage.FirstOrDefault() ?? "az";
            return Task.FromResult(new ProviderCultureResult(lang, lang));
        }
    }
}