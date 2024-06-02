using Coravel.Invocable;
using ScrappingService.Models;
using ScrappingService.Models.Entities;

namespace ScrappingService.Invoceables
{
    class CheckWeatherInvoceable : IInvocable
    {
        private readonly DataContext db;

        public CheckWeatherInvoceable(DataContext db)
        {
            this.db = db;
        }

        public async Task Invoke()
        {
            Console.WriteLine($"{db.Id}: [{this.GetType().Name}]: {DateTime.Now:yyyy.MM.dd HH:mm:ss}");

            for (int i = 0; i < 20; i++)
            {
                await db.Groups.AddAsync(new Group { Name = $"{this.GetType().Name} - Group-{i + 1}" });
            }
            await db.SaveChangesAsync();
        }
    }
}
