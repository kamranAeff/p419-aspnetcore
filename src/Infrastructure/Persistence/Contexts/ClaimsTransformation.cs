using Domain.Entities.Membership;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Persistence.Contexts
{
    public class ClaimsTransformation(DbContext db) : IClaimsTransformation
    {
        public static string[] policies = null;
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if (principal.Identity is ClaimsIdentity identity && identity.IsAuthenticated
                && int.TryParse(identity.Claims.FirstOrDefault(m => m.Type.Equals(ClaimTypes.NameIdentifier))?.Value ?? "", out int userId))
            {
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

            return principal;
        }
    }
}
