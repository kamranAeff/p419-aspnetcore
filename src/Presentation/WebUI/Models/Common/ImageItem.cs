namespace WebUI.Models.Common
{
    public class ImageItem
    {
        public int? Id { get; set; }
        public IFormFile File { get; set; }
        public string TempPath { get; set; }
        public bool IsMain { get; set; }
    }
}
