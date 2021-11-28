using System;
using System.Threading.Tasks;
using annoying.client.Services;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Polly;

namespace annoying.client.Extensions
{
    public static class HostExtensions
    {
        // public async static Task<IHost> MigrateAndSeedDatabase<T>(this IHost host) where T : DbContext
        public static IHost MigrateAndSeedDatabase<T>(this IHost host, int retries = 3) where T : DbContext
        {
            var policy = Policy.Handle<SqlException>().WaitAndRetry(retries, times => TimeSpan.FromMilliseconds(times * 100));
            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<T>();
                policy.Execute(() =>
                {
                    context.Database.Migrate();
                    context.Database.EnsureCreatedAsync();
                    SeedData.Initialize(scope.ServiceProvider)
                             .Wait();
                    // await context.Database.MigrateAsync();
                    // await context.Database.EnsureCreatedAsync();
                    // await SeedData.Initialize(scope.ServiceProvider);
                });

            }
            return host;
        }
    }
}