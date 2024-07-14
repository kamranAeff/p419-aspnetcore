using Microsoft.AspNetCore.Identity;

namespace Persistence.Contexts
{
    class ErrorDescriber : IdentityErrorDescriber
    {
        public override IdentityError PasswordRequiresUpper()
        {
            return new IdentityError
            {
                Code = nameof(PasswordRequiresUpper),
                Description = "Boyuk herf qeyd edilmelidir ('A'-'Z')"
            };
        }

        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = nameof(DuplicateEmail),
                Description = "Bu email artiq qeydiyyatlidir"
            };
        }
    }
}
