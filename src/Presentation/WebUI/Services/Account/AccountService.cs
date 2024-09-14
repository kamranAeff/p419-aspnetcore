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

        public async Task<ApiResponse<SignInResponseDto>> SignInAsync(SignInRequestDto request, CancellationToken cancellation = default)
        {
            var requestContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, MediaTypeNames.Application.Json);
            var response = await client.PostAsync("/api/account/signin", requestContent, cancellation);

            if (!response.IsSuccessStatusCode)
                throw new BadHttpRequestException("HTTP_CLIENT");

            var content = await response.Content.ReadAsStringAsync(cancellation);
            var apiResponse = JsonConvert.DeserializeObject<ApiResponse<SignInResponseDto>>(content)!;

            if (!apiResponse.IsSuccess)
                throw new BadHttpRequestException("HTTP_CLIENT");

            return apiResponse;
        }
    }
}
