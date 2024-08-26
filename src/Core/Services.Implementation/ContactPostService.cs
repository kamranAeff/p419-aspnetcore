using Domain.Entities;
using Repositories;

namespace Services.Implementation
{
    public class ContactPostService(IContactPostRepository contactPostRepository) : IContactPostService
    {

        public async Task<string> Add(string fullName, string email, string message)
        {
            var post = new ContactPost
            {
                FullName = fullName,
                Email = email,
                Message = message
            };

            await contactPostRepository.AddAsync(post);
            await contactPostRepository.SaveAsync();

            return "Muracietiniz qebul edildi.2 is gunu erzinde size geri doneceyik!";
        }
    }
}
