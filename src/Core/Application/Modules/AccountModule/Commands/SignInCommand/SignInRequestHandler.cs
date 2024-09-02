using Application.Extensions;
using Application.Services;
using Domain.Entities.Membership;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Modules.AccountModule.Commands.SignInCommand
{
    class SignInRequestHandler(UserManager<OganiUser> userManager,
        SignInManager<OganiUser> signInManager,
        ICryptoService cryptoService) : IRequestHandler<SignInRequest, SignInResponse>
    {
        public async Task<SignInResponse> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            string key = Environment.GetEnvironmentVariable("JWT__KEY")!;
            string issuer = Environment.GetEnvironmentVariable("JWT__ISSUER")!;
            string audience = Environment.GetEnvironmentVariable("JWT__AUDIENCE")!;
            int minutes = Convert.ToInt32(Environment.GetEnvironmentVariable("JWT__EXPIRATIONDURATIONMINUTES"));

            var passwordHasher = new PasswordHasher<OganiUser>();

            var user = request.UserName switch
            {
                _ when request.UserName.IsMail() => await userManager.FindByEmailAsync(request.UserName),
                _ when request.UserName.IsPhone() => await userManager.Users.FirstOrDefaultAsync(m => m.PhoneNumberConfirmed && request.UserName.Equals(m.PhoneNumber)),
                _ => await userManager.FindByNameAsync(request.UserName)
            };

            if (user is null)
                throw new UserNameOrPasswordIncorrectException();

            var password = passwordHasher.HashPassword(user, request.Password);

            var checkPasswordResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (checkPasswordResult.IsLockedOut)
                throw new AccountLockoutException();

            if (!checkPasswordResult.Succeeded)
                throw new UserNameOrPasswordIncorrectException();

            if (request.UserName.IsMail() && !user.EmailConfirmed)
                throw new UnverifiedEmailException();

            if (request.UserName.IsPhone() && !user.PhoneNumberConfirmed)
                throw new UnverifiedEmailException();

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer, audience,
            claims: [
                new(JwtRegisteredClaimNames.NameId, $"{user.Id}")
                ],
            expires: DateTime.UtcNow.AddDays(50),//DateTime.UtcNow.AddMinutes(minutes),
            signingCredentials: credentials);

            var response = new SignInResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };

            response.RefreshToken = cryptoService.Sha1Hash($"DEMO_{response.AccessToken}_@APP");

            return response;
        }
    }
}
