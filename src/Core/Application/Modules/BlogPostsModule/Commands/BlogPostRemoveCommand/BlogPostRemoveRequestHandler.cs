using MediatR;
using Repositories;
using Services.Common;

namespace Application.Modules.BlogPostsModule.Commands.BlogPostRemoveCommand
{
    class BlogPostRemoveRequestHandler(IBlogPostRepository blogPostRepository,IFileService fileService) : IRequestHandler<BlogPostRemoveRequest>
    {
        public async Task Handle(BlogPostRemoveRequest request, CancellationToken cancellationToken)
        {
            var entity = await blogPostRepository.GetAsync(m => m.Id == request.Id, cancellationToken);

            await fileService.ArchiveAsync($"{entity.Id}", entity.ImagePath, cancellationToken);
            blogPostRepository.Remove(entity);
            await blogPostRepository.SaveAsync(cancellationToken);
        }
    }
}
