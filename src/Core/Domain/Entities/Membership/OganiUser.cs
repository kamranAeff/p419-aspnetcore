using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Membership
{
    public class OganiUser : IdentityUser<int>
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public bool IsSubscriber { get; set; }
    }
}
