using Microsoft.AspNetCore.Http;

namespace Application.Common
{
    public class ImageItem
    {
        public int? Id { get; set; }
        public string TempPath { get; set; }
        public IFormFile File { get; set; }
        public bool IsMain { get; set; }
    }

    public class ImageItemBase64
    {
        public int? Id { get; set; }
        public string TempPath { get; set; }
        public string File { get; set; }
        public string FileName { get; set; }
        public bool IsMain { get; set; }
    }
}
