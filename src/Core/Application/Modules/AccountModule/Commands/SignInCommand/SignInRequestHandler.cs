using Application.Extensions;
using Domain.Entities.Membership;
using Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Services.Common;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Application.Modules.AccountModule.Commands.SignInCommand
{
    class SignInRequestHandler(UserManager<OganiUser> userManager,
        SignInManager<OganiUser> signInManager,
        ICryptoService cryptoService) : IRequestHandler<SignInRequest, SignInResponse>
    {
        public async Task<SignInResponse> Handle(SignInRequest request, CancellationToken cancellationToken)
        {
            var user = request.UserName switch
            {
                _ when request.UserName.IsMail() => await userManager.FindByEmailAsync(request.UserName),
                _ when request.UserName.IsPhone() => await userManager.Users.FirstOrDefaultAsync(m => m.PhoneNumberConfirmed && request.UserName.Equals(m.PhoneNumber)),
                _ => await userManager.FindByNameAsync(request.UserName)
            };

            if (user is null)
                throw new UserNameOrPasswordIncorrectException();

            var checkPasswordResult = await signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (checkPasswordResult.IsLockedOut)
                throw new AccountLockoutException();

            if (!checkPasswordResult.Succeeded)
                throw new UserNameOrPasswordIncorrectException();

            Claim[] claims = [new(JwtRegisteredClaimNames.NameId, $"{user.Id}")];

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("88b0ba2aaff3daa419917a9a1c85732570c4771b"));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken("issuer@ogani.az", "audience@ogani.az",
            claims,
            expires: DateTime.UtcNow.AddMinutes(10),
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
