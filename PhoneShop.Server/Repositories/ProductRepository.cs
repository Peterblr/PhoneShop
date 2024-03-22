using Microsoft.EntityFrameworkCore;
using PhoneShop.Server.Data;
using PhoneShop.Shared.Interfaces;
using PhoneShop.Shared.Models;
using PhoneShop.Shared.Services;

namespace PhoneShop.Server.Repositories
{
    public class ProductRepository : IProduct
    {
        private readonly AppDbContext appDbContext;

        public ProductRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<List<Product>> GetAllProducts(bool featuredProducts)
        {
            if (featuredProducts)
                return await appDbContext.Products.Where(_ => _.Featured).ToListAsync();
            else
                return await appDbContext.Products.ToListAsync();
        }

        public async Task<ServiceResponse> AddProduct(Product product)
        {
            if (product is null) return new ServiceResponse(false, "Product is null");

            var (flag, message) = await CheckName(product.Name!);

            if (flag)
            {
                appDbContext.Products.Add(product);
                await Commit();
                return new ServiceResponse(true, "Product Saved");
            }
            return new ServiceResponse(flag, message);
        }

        private async Task<ServiceResponse> CheckName(string name)
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Name.ToLower()!.Equals(name.ToLower()));
            return product is null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Product already exist");
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
