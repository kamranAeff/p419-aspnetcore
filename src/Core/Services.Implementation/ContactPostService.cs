using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Services.Implementation
{
    public class ContactPostService : IContactPostService
    {
        private readonly DbContext db;

        public ContactPostService(DbContext db)
        {
            this.db = db;
        }

        public string Add(string fullName, string email, string message)
        {
            var post = new ContactPost
            {
                FullName = fullName,
                Email = email,
                Message = message
            };

            db.Set<ContactPost>().Add(post);
            db.SaveChangesAsync().Wait();

            return "Muracietiniz qebul edildi.2 is gunu erzinde size geri doneceyik!";
        }
    }
}
