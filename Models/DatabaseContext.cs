using Microsoft.EntityFrameworkCore;

namespace LK2.Models
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<Link> Links { get; set; }
    }
}
