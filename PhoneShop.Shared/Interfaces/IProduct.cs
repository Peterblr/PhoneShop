using PhoneShop.Shared.Models;
using PhoneShop.Shared.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneShop.Shared.Interfaces
{
    public interface IProduct
    {
        Task<List<Product>> GetAllProducts(bool featuredProducts);
        Task<ServiceResponse> AddProduct(Product product);
    }
}
