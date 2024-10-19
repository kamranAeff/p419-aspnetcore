using Application.Extensions;
using Application.Services;
using Domain.Entities.Membership;
using Domain.Exceptions;
using Domain.StableModels;
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
        DbContext db,
        ICryptoService cryptoService) : IRequestHandler<SignInRequest, AuthenticateResponse>
    {
        public async Task<AuthenticateResponse> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            request.RememberMe = true;

            string key = Environment.GetEnvironmentVariable("JWT__KEY")!;
            string issuer = Environment.GetEnvironmentVariable("JWT__ISSUER")!;
            string audience = Environment.GetEnvironmentVariable("JWT__AUDIENCE")!;
            int minutes = Convert.ToInt32(Environment.GetEnvironmentVariable("JWT__EXPIRATIONDURATIONMINUTES"));

            //var passwordHasher = new PasswordHasher<OganiUser>();

            var user = request.UserName switch
            {
                _ when request.UserName.IsMail() => await userManager.FindByEmailAsync(request.UserName),
                _ when request.UserName.IsPhone() => await userManager.Users.FirstOrDefaultAsync(m => m.PhoneNumberConfirmed && request.UserName.Equals(m.PhoneNumber)),
                _ => await userManager.FindByNameAsync(request.UserName)
            };

            if (user is null)
                throw new UserNameOrPasswordIncorrectException();

            //var password = passwordHasher.HashPassword(user, request.Password);

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

            var accessTokenExpireDate = DateTime.UtcNow.AddMinutes(minutes);
            var token = new JwtSecurityToken(issuer, audience,
            claims: [
                new(JwtRegisteredClaimNames.NameId, $"{user.Id}")
                ],
            expires: accessTokenExpireDate,
            signingCredentials: credentials);

            var response = new AuthenticateResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };

            response.RefreshToken = request.RememberMe ? cryptoService.Sha1Hash($"DEMO_{response.AccessToken}_@APP") : null;

            await db.Set<OganiUserToken>().AddRangeAsync([
                new OganiUserToken{ UserId =user.Id, LoginProvider="APPLICATION",Name="ACCESS_TOKEN",Value =cryptoService.Sha1Hash(response.AccessToken),Type=TokenType.AccessToken,ExpireDate=accessTokenExpireDate,IsDisable=false  },
                new OganiUserToken{ UserId =user.Id, LoginProvider="APPLICATION",Name="REFRESH_TOKEN",Value =cryptoService.Sha1Hash(response.RefreshToken),Type=TokenType.RefreshToken,ExpireDate=accessTokenExpireDate.AddHours(2),IsDisable=false  },
                ], cancellationToken);

            await db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
