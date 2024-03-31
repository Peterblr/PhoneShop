using PhoneShop.Shared.Models;
using PhoneShop.Shared.Services;

namespace PhoneShop.Server.Repositories
{
    public interface IProduct
    {
        Task<List<Product>> GetAllProducts(bool featuredProducts);
        Task<ServiceResponse> AddProduct(Product product);
    }
}
