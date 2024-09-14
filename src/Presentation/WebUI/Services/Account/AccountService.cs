using Newtonsoft.Json;
using System.Net.Mime;
using System.Text;
using WebUI.Models.Common;
using WebUI.Models.DTOs.Account;
using WebUI.Services.Common;

namespace WebUI.Services.Account
{
    public class AccountService : ProxyService, IAccountService
    {
        public AccountService(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
            : base(httpClientFactory, configuration)
        {
        }

        public Task<ApiResponse<AuthenticateResponseDto>> SignInAsync(SignInRequestDto request, CancellationToken cancellation = default)
            => base.PostAsync<SignInRequestDto, ApiResponse<AuthenticateResponseDto>>("/api/account/signin", request, cancellation);

        public  Task<ApiResponse<AuthenticateResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellation = default)
        => base.PostAsync<RefreshTokenRequestDto, ApiResponse<AuthenticateResponseDto>>("/api/account/refresh-token", request, cancellation);

    }
}
