using WebUI.Models.Common;
using WebUI.Models.DTOs.Account;

namespace WebUI.Services.Account
{
    public interface IAccountService
    {
        Task<ApiResponse<SignInResponseDto>> SignInAsync(SignInRequestDto request, CancellationToken cancellation = default);
    }
}
