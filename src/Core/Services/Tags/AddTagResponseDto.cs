
using Domain.Entities;

namespace Services.Tags
{
    public class AddTagResponseDto
    {
        public int Id { get; set; }
        public required string Text { get; set; }

        public static AddTagResponseDto Create(Tag entity)
        {
            return new AddTagResponseDto
            {
                Id = entity.Id,
                Text = entity.Text
            };
        }
    }
}
