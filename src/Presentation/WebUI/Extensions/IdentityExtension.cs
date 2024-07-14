using System.Security.Claims;

namespace WebUI.Extensions
{
    public static partial class Extension
    {
        public static bool HasAccess(this ClaimsPrincipal user,string claim)
        {
            return user.IsInRole("SuperAdmin") || user.HasClaim(claim, "1");
        }
    }
}
