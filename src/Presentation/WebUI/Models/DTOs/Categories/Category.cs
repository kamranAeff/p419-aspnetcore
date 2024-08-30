using Newtonsoft.Json;

namespace WebUI.Models.DTOs.Categories
{
    public class Category
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
