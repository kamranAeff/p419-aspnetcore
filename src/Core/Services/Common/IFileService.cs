using Microsoft.AspNetCore.Http;

namespace Services.Common
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file, CancellationToken cancellation = default);
    }
}
