using Domain.Entities;
using MediatR;
using Repositories;

namespace Application.Modules.ContactPostsModule.Commands.ContactPostApplyCommand
{
    class ContactPostApplyRequestHandler(IContactPostRepository contactPostRepository) : IRequestHandler<ContactPostApplyRequest>
    {
        public async Task Handle(ContactPostApplyRequest request, CancellationToken cancellationToken)
        {
            var entity = new ContactPost
            {
                FullName = request.FullName,
                Email = request.Email,
                Message = request.Message
            };
            await contactPostRepository.AddAsync(entity,cancellationToken);
            await contactPostRepository.SaveAsync(cancellationToken);
        }
    }
}
