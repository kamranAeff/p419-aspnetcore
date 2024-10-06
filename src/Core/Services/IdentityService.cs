using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Services
{
    public class IdentityService(IHttpContextAccessor ctx) : IIdentityService
    {
        public int UserId =>
            ctx.HttpContext?.User?.Identity?.IsAuthenticated == true
            && int.TryParse(ctx.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId)
       ? userId
       : throw new UnauthorizedException();
    }
}
