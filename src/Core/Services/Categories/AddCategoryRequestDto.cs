namespace Services.Categories
{
    public class AddCategoryRequestDto
    {
        public required string Name { get; set; }
    }
    public class AddCategoryResponseDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
    }
}
