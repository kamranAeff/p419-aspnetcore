using Application.Services;
using Domain.Entities.Membership;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Modules.AccountModule.Commands.RefreshTokenCommand
{
    class RefreshTokenRequestHandler(ICryptoService cryptoService, UserManager<OganiUser> userManager) 
        : IRequestHandler<RefreshTokenRequest, AuthenticateResponse>
    {
        public async Task<AuthenticateResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            if (!request.RefreshToken.Equals(cryptoService.Sha1Hash($"DEMO_{request.AccessToken}_@APP")))
                throw new UnauthorizedAccessException();

            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenInfo = tokenHandler.ReadJwtToken(request.AccessToken);

            var userId = tokenInfo.Claims.FirstOrDefault(m => m.Type.Equals(JwtRegisteredClaimNames.NameId))?.Value;

            if (userId is null || !int.TryParse(userId,out int numUserId))
                throw new UnauthorizedAccessException();

            var user = await userManager.Users.FirstOrDefaultAsync(m=>m.Id == numUserId);

            if (user is null)
                throw new UnauthorizedAccessException();


            string key = Environment.GetEnvironmentVariable("JWT__KEY")!;
            string issuer = Environment.GetEnvironmentVariable("JWT__ISSUER")!;
            string audience = Environment.GetEnvironmentVariable("JWT__AUDIENCE")!;
            int minutes = Convert.ToInt32(Environment.GetEnvironmentVariable("JWT__EXPIRATIONDURATIONMINUTES"));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer, audience,
            claims: [
                new(JwtRegisteredClaimNames.NameId, $"{user.Id}")
                ],
            expires: DateTime.UtcNow.AddMinutes(minutes),
            signingCredentials: credentials);

            var response = new AuthenticateResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token)
            };

            response.RefreshToken = cryptoService.Sha1Hash($"DEMO_{response.AccessToken}_@APP");

            return response;
        }
    }
}
