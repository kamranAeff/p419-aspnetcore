using Application.Services;
using MediatR;
using Repositories;

namespace Application.Modules.ContactPostsModule.Commands.ContactPostReplyCommand
{
    class ContactPostReplyRequestHandler(IEmailService emailService,
        IContactPostRepository contactPostRepository)
        : IRequestHandler<ContactPostReplyRequest>
    {
        public async Task Handle(ContactPostReplyRequest request, CancellationToken cancellationToken)
        {
            var post = await contactPostRepository.GetAsync(m => m.Id == request.Id, cancellationToken);
            post.AnsweredMessage = request.Message;
            post.AnsweredAt = DateTime.Now;
            post.AnsweredBy = 1;
            await contactPostRepository.SaveAsync(cancellationToken);

            await emailService.SendEmailQueue(post.Email, "Ogani Support", request.Message);
        }
    }
}
