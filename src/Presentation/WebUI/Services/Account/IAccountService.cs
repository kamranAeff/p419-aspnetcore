using WebUI.Models.Common;
using WebUI.Models.DTOs.Account;

namespace WebUI.Services.Account
{
    public interface IAccountService
    {
        Task<ApiResponse<AuthenticateResponseDto>> SignInAsync(SignInRequestDto request, CancellationToken cancellation = default);
        Task<ApiResponse<AuthenticateResponseDto>> RefreshTokenAsync(RefreshTokenRequestDto request, CancellationToken cancellation = default);
    }
}