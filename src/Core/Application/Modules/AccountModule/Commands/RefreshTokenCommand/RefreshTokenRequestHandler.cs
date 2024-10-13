using Application.Services;
using Domain.Entities.Membership;
using Domain.Exceptions;
using Domain.StableModels;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Application.Modules.AccountModule.Commands.RefreshTokenCommand
{
    class RefreshTokenRequestHandler(ICryptoService cryptoService, UserManager<OganiUser> userManager, DbContext db, IHttpContextAccessor ctx)
        : IRequestHandler<RefreshTokenRequest, AuthenticateResponse>
    {
        public async Task<AuthenticateResponse> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            if (!ctx.HttpContext.Request.Headers.TryGetValue("token", out StringValues refreshTokens))
                throw new BadRequestException("BADREG", new Dictionary<string, IEnumerable<string>>
                {
                    ["RefreshToken"] = ["RefreshToken cant be null"]
                });

            if (!ctx.HttpContext.Request.Headers.TryGetValue("Authorization", out StringValues accessTokens))
                throw new BadRequestException("BADREG", new Dictionary<string, IEnumerable<string>>
                {
                    ["AccessToken"] = ["AccessToken cant be null"]
                });


            request.RefreshToken = refreshTokens.FirstOrDefault();
            request.AccessToken = accessTokens.FirstOrDefault()?.Replace("Bearer ", string.Empty);

            if (!request.RefreshToken.Equals(cryptoService.Sha1Hash($"DEMO_{request.AccessToken}_@APP")))
                throw new UnauthorizedAccessException();


            var tokenHandler = new JwtSecurityTokenHandler();

            var tokenInfo = tokenHandler.ReadJwtToken(request.AccessToken);

            var userId = tokenInfo.Claims.FirstOrDefault(m => m.Type.Equals(JwtRegisteredClaimNames.NameId))?.Value;

            if (userId is null || !int.TryParse(userId, out int numUserId))
                throw new UnauthorizedAccessException();


            #region Access Token Check
            var accessTokenHash = cryptoService.Sha1Hash(request.AccessToken);

            var accessTokenEntry = await db.Set<OganiUserToken>().FirstOrDefaultAsync(m => m.UserId == numUserId &&
            m.Type == TokenType.AccessToken &&
            accessTokenHash.Equals(m.Value) &&
            "APPLICATION".Equals(m.LoginProvider), cancellationToken);


            if (accessTokenEntry is null)
                throw new BadRequestException("BADREG", new Dictionary<string, IEnumerable<string>>
                {
                    ["AccessToken"] = ["AccessToken is modified"]
                });
            else if (accessTokenEntry.IsDisable)
                throw new BadRequestException("BADREG", new Dictionary<string, IEnumerable<string>>
                {
                    ["AccessToken"] = ["AccessToken is disabled"]
                });
            #endregion


            #region Access Token Check
            var refreshTokenHash = cryptoService.Sha1Hash(request.RefreshToken);

            var refreshTokenEntry = await db.Set<OganiUserToken>().FirstOrDefaultAsync(m => m.UserId == numUserId &&
                                           m.Type == TokenType.RefreshToken &&
                                           refreshTokenHash.Equals(m.Value) &&
                                           "APPLICATION".Equals(m.LoginProvider), cancellationToken);


            if (refreshTokenEntry is null)
                throw new BadRequestException("BADREG", new Dictionary<string, IEnumerable<string>>
                {
                    ["RefreshToken"] = ["RefreshToken is modified"]
                });
            else if (refreshTokenEntry.IsDisable || refreshTokenEntry.ExpireDate < DateTime.UtcNow)
                throw new BadRequestException("BADREG", new Dictionary<string, IEnumerable<string>>
                {
                    ["RefreshToken"] = ["RefreshToken is disabled"]
                });
            #endregion

            var user = await userManager.Users.FirstOrDefaultAsync(m => m.Id == numUserId);

            if (user is null)
                throw new UnauthorizedAccessException();


            string key = Environment.GetEnvironmentVariable("JWT__KEY")!;
            string issuer = Environment.GetEnvironmentVariable("JWT__ISSUER")!;
            string audience = Environment.GetEnvironmentVariable("JWT__AUDIENCE")!;
            int minutes = Convert.ToInt32(Environment.GetEnvironmentVariable("JWT__EXPIRATIONDURATIONMINUTES"));

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

            response.RefreshToken = cryptoService.Sha1Hash($"DEMO_{response.AccessToken}_@APP");


            accessTokenEntry.IsDisable = true;
            refreshTokenEntry.IsDisable = true;

            await db.Set<OganiUserToken>().AddRangeAsync([
                new OganiUserToken{ UserId =user.Id, LoginProvider="APPLICATION",Name="ACCESS_TOKEN",Value =cryptoService.Sha1Hash(response.AccessToken),Type=TokenType.AccessToken,ExpireDate=accessTokenExpireDate,IsDisable=false  },
                new OganiUserToken{ UserId =user.Id, LoginProvider="APPLICATION",Name="REFRESH_TOKEN",Value =cryptoService.Sha1Hash(response.RefreshToken),Type=TokenType.RefreshToken,ExpireDate=accessTokenExpireDate.AddHours(2),IsDisable=false  },
                ], cancellationToken);

            await db.SaveChangesAsync(cancellationToken);

            return response;
        }
    }
}
