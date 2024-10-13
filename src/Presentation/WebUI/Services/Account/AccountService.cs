using Azure;
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

        public  async Task<ApiResponse<AuthenticateResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellation = default)
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Post, "/api/account/refresh-token");
            requestMessage.Headers.TryAddWithoutValidation("token", request.RefreshToken);
            var response = await client.SendAsync(requestMessage,cancellation);

            var contentJsonContent = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<AuthenticateResponseDto>>(contentJsonContent)!;

            return apiResponse;

            //base.PostAsync<RefreshTokenRequestDto, ApiResponse<AuthenticateResponseDto>>("/api/account/refresh-token", request, cancellation)
        }

    }
}
