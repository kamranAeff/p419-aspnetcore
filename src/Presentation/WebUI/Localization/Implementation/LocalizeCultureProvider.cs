﻿using Microsoft.AspNetCore.Localization;

namespace WebUI.Localization.Implementation
{
    public class LocalizeCultureProvider : RequestCultureProvider
    {
        public override Task<ProviderCultureResult> DetermineProviderCultureResult(HttpContext context)
        {
            var lang = context.GetRouteValue("lang")?.ToString() ?? "az";
            return Task.FromResult(new ProviderCultureResult(lang, lang));
        }
    }
}
