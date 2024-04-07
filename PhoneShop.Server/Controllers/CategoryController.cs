using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.Server.Repositories;
using PhoneShop.Shared.Models;
using PhoneShop.Shared.Services;

namespace PhoneShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategory categoryService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Category>>> GetAllCategories()
        {
            var categories = await categoryService.GetAllCategories();    

            return Ok(categories);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddCategory(Category category)
        {
            if (category == null) return BadRequest("Category is null");

            var response = await categoryService.AddCategory(category);

            return Ok(response);
        }
    }
}
