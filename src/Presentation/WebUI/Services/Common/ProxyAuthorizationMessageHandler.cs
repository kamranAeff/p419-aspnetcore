
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace WebUI.Services.Common
{
    public class ProxyAuthorizationMessageHandler(IActionContextAccessor ctx) : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (ctx.ActionContext.HttpContext.Request.Cookies.TryGetValue("accessToken",out string token))
                request.Headers.TryAddWithoutValidation("Authorization", $"Bearer {token}");


            var response =  base.SendAsync(request, cancellationToken);
            //after

            return response;
        }
    }
}
