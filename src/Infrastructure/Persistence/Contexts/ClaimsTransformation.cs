using Application.Services;
using Azure.Core;
using Domain.Entities.Membership;
using Domain.Exceptions;
using Domain.StableModels;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using System.Security.Claims;
using System.Threading;

namespace Persistence.Contexts
{
    public class ClaimsTransformation(DbContext db, ICryptoService cryptoService, IHttpContextAccessor ctx) : IClaimsTransformation
    {
        public static string[] policies = null;
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is ClaimsIdentity identity && identity.IsAuthenticated
                && int.TryParse(identity.Claims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? "", out int userId))
            {
                #region Access Token is live or not
                ctx.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues accessTokens);

                string accessToken = accessTokens.FirstOrDefault()?.Replace("Bearer ", string.Empty)!;

                var accessTokenHash = cryptoService.Sha1Hash(accessToken);

                var accessTokenEntry = await db.Set<OganiUserToken>().FirstOrDefaultAsync(m => m.UserId == userId &&
                m.Type == TokenType.AccessToken &&
                accessTokenHash.Equals(m.Value) &&
                "APPLICATION".Equals(m.LoginProvider));

                if (accessTokenEntry is null || accessTokenEntry.IsDisable)
                {
                    ctx.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    await ctx.HttpContext.Response.WriteAsJsonAsync(new
                    {
                        code = ctx.HttpContext.Response.StatusCode,
                        message = "token_expired"
                    });
                    await ctx.HttpContext.Response.CompleteAsync();
                    goto l1;
                }
                #endregion


                var queryRoles = from r in db.Set<OganiRole>()
                                 join ur in db.Set<OganiUserRole>() on r.Id equals ur.RoleId
                                 where ur.UserId == userId
                                 select r;

                #region Load Roles
                var roles = await queryRoles.Select(m => m.NormalizedName).ToListAsync();

                foreach (var role in roles)
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                #endregion

                #region Load Claims
                var claims = await (from r in queryRoles
                                    join rc in db.Set<OganiRoleClaim>() on r.Id equals rc.RoleId
                                    where "1".Equals(rc.ClaimValue)
                                    select rc.ClaimType)
                             .Union(from uc in db.Set<OganiUserClaim>()
                                    where "1".Equals(uc.ClaimValue) && uc.UserId == userId
                                    select uc.ClaimType).ToListAsync();

                foreach (var claimType in claims)
                {
                    identity.AddClaim(new Claim(claimType, "1"));
                }
                #endregion
            }

        l1:
            return principal;
        }
    }
}
