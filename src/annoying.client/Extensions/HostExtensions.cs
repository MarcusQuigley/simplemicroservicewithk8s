using System.Threading.Tasks;
using annoying.client.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace annoying.client.Extensions
{
    public static class HostExtensions
    {
        // public async static Task<IHost> MigrateAndSeedDatabase<T>(this IHost host) where T : DbContext
        public static IHost MigrateAndSeedDatabase<T>(this IHost host) where T : DbContext
        {
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<T>();
                context.Database.Migrate();
                context.Database.EnsureCreatedAsync();
                SeedData.Initialize(scope.ServiceProvider).Wait();
                // await context.Database.MigrateAsync();
                // await context.Database.EnsureCreatedAsync();
                // await SeedData.Initialize(scope.ServiceProvider);
            }
            return host;
        }
    }
}