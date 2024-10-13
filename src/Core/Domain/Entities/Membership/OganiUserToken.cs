using Domain.StableModels;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Membership
{
    public class OganiUserToken : IdentityUserToken<int>
    {
        public TokenType Type { get; set; }
        public DateTime ExpireDate { get; set; }
        public bool IsDisable { get; set; }
    }
}
