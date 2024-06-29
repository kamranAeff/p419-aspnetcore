using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;
using Services.Tags;

namespace Services.Implementation
{
    class TagService : ITagService
    {
        private readonly ITagRepository tagRepository;

        public TagService(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        public async Task<AddTagResponseDto> AddAsync(AddTagRequestDto model, CancellationToken cancellationToken = default)
        {
            var tag = new Tag
            {
                Text = model.Text
            };

            await tagRepository.AddAsync(tag, cancellationToken);
            await tagRepository.SaveAsync(cancellationToken);

            return AddTagResponseDto.Create(tag);
        }

        public async Task<IEnumerable<TagGetAll>> GetAllASync(CancellationToken cancellationToken = default)
        {
            return await tagRepository.GetAll()
                .Select(m => new TagGetAll
                {
                    Id = m.Id,
                    Text = m.Text
                })
                .ToListAsync(cancellationToken);
        }
    }
}
