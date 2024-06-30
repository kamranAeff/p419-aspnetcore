namespace Services.Tags
{
    public interface ITagService
    {
        Task<AddTagResponseDto> AddAsync(AddTagRequestDto model, CancellationToken cancellationToken = default);
        Task<IEnumerable<TagGetAll>> GetAllASync(CancellationToken cancellationToken = default);
        Task<TagGetAll> GetById(int id, CancellationToken cancellationToken = default);
        Task<TagGetAll> GetByName(string name, CancellationToken cancellationToken = default);
    }
}
