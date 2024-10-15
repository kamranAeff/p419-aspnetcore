using WebUI.Models.Common;
using WebUI.Models.DTOs.ContactPosts;
using WebUI.Services.Common;

namespace WebUI.Services.ContactPosts
{
    public class ContactPostsService : ProxyService, IContactPostsService
    {
        public ContactPostsService(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
            : base(httpClientFactory, configuration)
        {
        }

        public Task<ApiResponse> ApplyAsync(ContactPostApplyDto request, CancellationToken cancellation = default)
            => base.PostAsync<ContactPostApplyDto,ApiResponse>("/api/contactposts", request,cancellation);
    }
}