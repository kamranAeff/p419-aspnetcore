using Application.Services;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.IdentityModel.Tokens.Jwt;

namespace Services
{
    class IdentityService(IActionContextAccessor ctx) : IIdentityService
    {
        public int UserId =>
            ctx.ActionContext?.HttpContext?.User?.Identity?.IsAuthenticated == true
            && int.TryParse(ctx.ActionContext?.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.NameId)?.Value, out var userId)
       ? userId
       : throw new UnauthorizedException();
    }
}
