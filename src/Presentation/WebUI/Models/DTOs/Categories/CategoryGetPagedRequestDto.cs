using Microsoft.Data.SqlClient;

namespace WebUI.Models.DTOs.Categories
{
    public class CategoryGetPagedRequestDto
    {
        public IEnumerable<int> Id { get; set; }
        public string Name { get; set; }

        public string Column { get; set; }

        public string Order { get; set; }
    }
}
