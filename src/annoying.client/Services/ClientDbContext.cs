using Microsoft.EntityFrameworkCore;

namespace annoying.client.Services
{
    public class ClientDbContext : DbContext
    {

        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {

        }
        public DbSet<Player> Players { get; set; }

    }
}