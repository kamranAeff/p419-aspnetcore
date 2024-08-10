using Microsoft.AspNetCore.Http;

namespace Services.Common
{
    public interface IFileService
    {
        Task<string> UploadAsync(IFormFile file, CancellationToken cancellation = default);
        Task<string> ChangeAsync(string identifier, string oldFileName, IFormFile file, CancellationToken cancellation = default);
        Task ArchiveAsync(string identifier, string oldFileName, CancellationToken cancellation = default);
    }
}
