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

        public async Task<ServiceResponce> AddProduct(Product product)
        {
            if (product is null) return new ServiceResponce(false, "Product is null");

            var (flag, message) = await CheckName(product.Name!);

            if (flag)
            {
                appDbContext.Products.Add(product);
                await Commit();
                return new ServiceResponce(true, "Product Saved");
            }
            return new ServiceResponce(flag, message);
        }

        private async Task<ServiceResponce> CheckName(string name)
        {
            var product = await appDbContext.Products.FirstOrDefaultAsync(x => x.Name.ToLower()!.Equals(name.ToLower()));
            return product is null ? new ServiceResponce(true, null!) : new ServiceResponce(false, "Product already exist");
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();
    }
}
