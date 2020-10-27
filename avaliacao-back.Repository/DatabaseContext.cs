using avaliacao_back.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace avaliacao_back.Repository
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> User { get; set; }
    }
}