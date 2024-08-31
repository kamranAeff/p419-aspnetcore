using Application.Extensions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repositories;
using Services.Common;

namespace Application.Modules.BlogPostsModule.Commands.BlogPostEditCommand
{
    class BlogPostEditRequestHandler : IRequestHandler<BlogPostEditRequest, BlogPostResponse>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IFileService fileService;
        private readonly IHttpContextAccessor ctx;

        public BlogPostEditRequestHandler(IBlogPostRepository blogPostRepository, IFileService fileService, IHttpContextAccessor ctx)
        {
            this.blogPostRepository = blogPostRepository;
            this.fileService = fileService;
            this.ctx = ctx;
        }

        public async Task<BlogPostResponse> Handle(BlogPostEditRequest request, CancellationToken cancellationToken)
        {
            var entity = await blogPostRepository.GetAsync(m => m.Id == request.Id, cancellationToken);

            #region FileUpload
            if (request.Image is not null && !string.IsNullOrWhiteSpace(entity.ImagePath))
            {
                entity.ImagePath = await fileService.ChangeAsync($"{entity.Id}", entity.ImagePath, request.Image, cancellationToken);
            }
            else if (request.Image is not null)
            {
                entity.ImagePath = await fileService.UploadAsync(request.Image, cancellationToken);
            }
            else if (request.Image is null && string.IsNullOrWhiteSpace(request.ImagePath))
            {
                entity.ImagePath = string.Empty;
                await fileService.ArchiveAsync($"{entity.Id}", entity.ImagePath, cancellationToken);
            }
            #endregion

            entity.Title = request.Title;
            entity.Body = request.Body;
            entity.CategoryId = request.CategoryId;

            if (string.IsNullOrWhiteSpace(entity.Slug))
            {
                entity.Slug = request.Title.ToSlug();
            }

            await blogPostRepository.SaveAsync(cancellationToken);

            return new BlogPostResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Slug = entity.Slug,
                Body = entity.Body,
                CategoryId = entity.CategoryId,
                Image = $"{ctx.GetHost()}/files/{entity.ImagePath}",
            };
        }
    }
}
