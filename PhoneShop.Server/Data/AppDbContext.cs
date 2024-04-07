using Microsoft.EntityFrameworkCore;
using PhoneShop.Shared.Models;

namespace PhoneShop.Server.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Product> Products { get; set; } = default!;
        public DbSet<Category> Categories { get; set; } = default!;
    }
}
