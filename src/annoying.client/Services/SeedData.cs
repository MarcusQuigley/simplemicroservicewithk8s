using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace annoying.client.Services
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {

            using (var dbContext = new ClientDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ClientDbContext>>()))
            {
                if (dbContext.Players.Any())
                {
                    Console.WriteLine("  database has already been seeded");
                    return;   // DB has been seeded
                }
                Console.WriteLine("Seeding Catalog database");
                await PopulateTestData(dbContext);

            }
        }

        private static async Task PopulateTestData(ClientDbContext dbContext)
        {
            string playerSql = "insert into players(  name, number) values " +
               " (  'Jota', 20), " +
               " (  'Salah', 11)," +
               " (  'Mane', 10), " +
               " ( 'Henderson', 14) ";

            await dbContext.Database.ExecuteSqlRawAsync(playerSql);
        }
    }
}