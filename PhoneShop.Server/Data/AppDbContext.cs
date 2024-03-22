using Microsoft.EntityFrameworkCore;
using PhoneShop.Shared.Models;

namespace PhoneShop.Server.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; } = default!;
    }
}
