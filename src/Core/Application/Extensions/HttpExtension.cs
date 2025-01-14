﻿using Microsoft.AspNetCore.Http;

namespace Application.Extensions
{
    public static partial class Extension
    {
        public static string GetHost(this IHttpContextAccessor ctx)
        {
            string host = Environment.GetEnvironmentVariable("SOURCE_URL");

            if (!string.IsNullOrWhiteSpace(host))
                return host;

            return $"{ctx.HttpContext.Request.Scheme}://{ctx.HttpContext.Request.Host}";
        }
    }
}
