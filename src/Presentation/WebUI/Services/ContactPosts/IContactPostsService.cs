using WebUI.Models.Common;
using WebUI.Models.DTOs.ContactPosts;

namespace WebUI.Services.ContactPosts
{
    public interface IContactPostsService
    {
        Task<ApiResponse> ApplyAsync(ContactPostApplyDto request, CancellationToken cancellation = default);
    }
}