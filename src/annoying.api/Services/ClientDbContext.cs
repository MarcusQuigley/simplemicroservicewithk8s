using Microsoft.EntityFrameworkCore;

namespace annoying.api.Services
{
    public class ClientDbContext : DbContext
    {

        public ClientDbContext(DbContextOptions<ClientDbContext> options) : base(options)
        {

        }
        public DbSet<Player> Players { get; set; }

    }
}