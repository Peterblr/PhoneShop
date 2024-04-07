using PhoneShop.Shared.Models;
using PhoneShop.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneShop.Client.Services
{
    public interface IProductService
    {
        Task<List<Product>> GetAllProducts(bool featuredProducts);
        Task<ServiceResponse> AddProduct(Product product);
    }
}
