using Microsoft.AspNetCore.Http;

namespace Application.Extensions
{
    public static partial class Extension
    {
        public static string GetHost(this IHttpContextAccessor ctx)
            => $"{ctx.HttpContext.Request.Scheme}://{ctx.HttpContext.Request.Host}";
    }
}
