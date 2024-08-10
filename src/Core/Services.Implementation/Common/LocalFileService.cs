using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Services.Common;
using Services.Implementation.Registration;

namespace Services.Implementation.Common
{
    [SingletonLifeTime]
    class LocalFileService(IHostEnvironment env) : IFileService
    {
        public async Task<string> UploadAsync(IFormFile file, CancellationToken cancellation = default)
        {
            var extension = Path.GetExtension(file.FileName); // .jpg,  .jpeg
            var fileName = $"{Guid.NewGuid()}{extension}"; //B1EA73E3-E115-4689-A77B-D74A35F0DD55.jpg
            string fullPath = Path.Combine(env.ContentRootPath, "wwwroot", "uploads", fileName);

            using (var fs = new FileStream(fullPath, FileMode.CreateNew, FileAccess.Write))
            {
                await file.CopyToAsync(fs, cancellation);
            }

            return fileName;
        }
    }
}
