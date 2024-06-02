using Coravel.Invocable;
using Microsoft.EntityFrameworkCore;
using ScrappingService.Models;
using ScrappingService.Models.Entities;
using System;

namespace ScrappingService.Invoceables
{
    class CurrencyInvoceable : IInvocable
    {
        private readonly DataContext db;
        Random random;

        public CurrencyInvoceable(DataContext db)
        {
            this.db = db;
            random = new Random();
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
