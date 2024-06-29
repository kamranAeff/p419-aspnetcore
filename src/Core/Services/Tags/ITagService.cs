namespace Services.Tags
{
    public interface ITagService
    {
        Task<AddTagResponseDto> AddAsync(AddTagRequestDto model, CancellationToken cancellationToken = default);
        Task<IEnumerable<TagGetAll>> GetAllASync(CancellationToken cancellationToken = default);
    }
}
