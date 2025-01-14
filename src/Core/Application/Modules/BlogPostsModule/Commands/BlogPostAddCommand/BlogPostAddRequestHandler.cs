﻿using Application.Extensions;
using Application.Services;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Repositories;

namespace Application.Modules.BlogPostsModule.Commands.BlogPostAddCommand
{
    class BlogPostAddRequestHandler : IRequestHandler<BlogPostAddRequest, BlogPostResponse>
    {
        private readonly IBlogPostRepository blogPostRepository;
        private readonly IFileService fileService;
        private readonly IHttpContextAccessor ctx;

        public BlogPostAddRequestHandler(IBlogPostRepository blogPostRepository, IFileService fileService, IHttpContextAccessor ctx)
        {
            this.blogPostRepository = blogPostRepository;
            this.fileService = fileService;
            this.ctx = ctx;
        }

        public async Task<BlogPostResponse> Handle(BlogPostAddRequest request, CancellationToken cancellationToken)
        {
            var fileName = await fileService.UploadAsync(request.Image, cancellationToken);

            var entity = new BlogPost
            {
                Title = request.Title,
                Slug = request.Title.ToSlug(),
                Body = request.Body,
                CategoryId = request.CategoryId,
                ImagePath = fileName,
            };

            await blogPostRepository.AddAsync(entity, cancellationToken);
            await blogPostRepository.SaveAsync(cancellationToken);

            return new BlogPostResponse
            {
                Id = entity.Id,
                Title = entity.Title,
                Slug = entity.Slug,
                Body = entity.Body,
                CategoryId = entity.CategoryId,
                Image = $"{ctx.GetHost()}/files/{fileName}",
            };
        }
    }
}
