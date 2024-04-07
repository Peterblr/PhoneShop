using PhoneShop.Shared.Models;
using PhoneShop.Shared.Services;

namespace PhoneShop.Server.Repositories
{
    public interface ICategory
    {
        Task<List<Category>> GetAllCategories();

        Task<ServiceResponse> AddCategory(Category category);
    }
}
