using Microsoft.AspNetCore.Mvc.Infrastructure;
using Newtonsoft.Json;
using System.Net;
using WebUI.Exceptions;
using WebUI.Models.Common;

namespace WebUI.Services.Common
{
    public class ProxyExceptionMessageHandler(IActionContextAccessor ctx) : DelegatingHandler
    {
        async protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            if (response.IsSuccessStatusCode)
                return response;

            switch (response.StatusCode)
            {
                case HttpStatusCode.Unauthorized when (!request.Headers.TryGetValues("raise401", out IEnumerable<string> raiseError) || !raiseError.Any()):
                    return response;
                case HttpStatusCode.Unauthorized:
                    ctx.ActionContext.HttpContext.Response.Redirect("/signin.html");
                    await ctx.ActionContext.HttpContext.Response.CompleteAsync();
                    break;
                case HttpStatusCode.Forbidden:
                    ctx.ActionContext.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                    ctx.ActionContext.HttpContext.Response.WriteAsync(File.ReadAllText("wwwroot/error-pages/403.html"));
                    break;
                case HttpStatusCode.NotFound:
                    if ("XMLHttpRequest".Equals(ctx.ActionContext.HttpContext.Request.Headers["X-Requested-With"]))
                        throw new NotFoundException();

                    ctx.ActionContext.HttpContext.Response.StatusCode = StatusCodes.Status200OK;
                    ctx.ActionContext.HttpContext.Response.WriteAsync(File.ReadAllText("wwwroot/error-pages/404.html"));
                    break;
                case HttpStatusCode.BadRequest:
                    var badRequestJson = await response.Content.ReadAsStringAsync(cancellationToken);
                    var badResponse = JsonConvert.DeserializeObject<ApiResponse>(badRequestJson);
                    throw new BadRequestException("BADREQ", badResponse.Errors);
                default:
                    throw new NotImplementedException();
            }

            return response;
        }
    }
}