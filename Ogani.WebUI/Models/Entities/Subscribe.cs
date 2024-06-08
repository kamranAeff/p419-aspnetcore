namespace Ogani.WebUI.Models.Entities
{
    public class Subscribe
    {
        public required string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ApprovedAt { get; set; }
    }
}
