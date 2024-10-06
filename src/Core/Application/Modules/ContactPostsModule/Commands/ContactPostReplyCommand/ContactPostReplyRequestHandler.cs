﻿using Application.Services;
using MediatR;
using Repositories;
using System.Text;

namespace Application.Modules.ContactPostsModule.Commands.ContactPostReplyCommand
{
    class ContactPostReplyRequestHandler(IEmailService emailService,
        IContactPostRepository contactPostRepository)
        : IRequestHandler<ContactPostReplyRequest>
    {
        public async Task Handle(ContactPostReplyRequest request, CancellationToken cancellationToken)
        {
            var post = await contactPostRepository.GetAsync(m => m.Id == request.Id
            && m.AnsweredAt == null, cancellationToken);

            post.AnsweredMessage = request.Message;
            post.AnsweredAt = DateTime.Now;
            post.AnsweredBy = 1;
            await contactPostRepository.SaveAsync(cancellationToken);

            var sb = new StringBuilder();

            sb.AppendLine($"Salam, {post.FullName},");

            sb.Append($"<b>Sualınız:</b><br/>{post.Message}<hr/>");
            sb.Append(post.AnsweredMessage);

            await emailService.SendEmailQueue(post.Email, "Ogani Support", sb.ToString());
        }
    }
}