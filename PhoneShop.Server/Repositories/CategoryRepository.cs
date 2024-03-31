using Microsoft.EntityFrameworkCore;
using PhoneShop.Client.Pages;
using PhoneShop.Server.Data;
using PhoneShop.Shared.Models;
using PhoneShop.Shared.Services;
using System;

namespace PhoneShop.Server.Repositories
{
    public class CategoryRepository(AppDbContext appDbContext) : ICategory
    {
        private readonly AppDbContext appDbContext = appDbContext;

        public async Task<List<Category>> GetAllCategories() => await appDbContext.Categories.ToListAsync();

        public async Task<ServiceResponse> AddCategory(Category category)
        {
            if (category is null) return new ServiceResponse(false, "Category is null");

            var (flag, message) = await CheckName(category.Name!);

            if (flag)
            {
                appDbContext.Categories.Add(category);
                await Commit();
                return new ServiceResponse(true, "Category Saved");
            }
            return new ServiceResponse(flag, message);
        }

        private async Task<ServiceResponse> CheckName(string name)
        {
            var category = await appDbContext.Categories.FirstOrDefaultAsync(x => x.Name.ToLower()!.Equals(name.ToLower()));
            return category is null ? new ServiceResponse(true, null!) : new ServiceResponse(false, "Category already exist");
        }

        private async Task Commit() => await appDbContext.SaveChangesAsync();

    }
}
