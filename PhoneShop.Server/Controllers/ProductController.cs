using Microsoft.AspNetCore.Mvc;
using PhoneShop.Server.Repositories;
using PhoneShop.Shared.Models;
using PhoneShop.Shared.Services;

namespace PhoneShop.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController(IProduct productService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts(bool featured)
        {
            var product = await productService.GetAllProducts(featured);
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ServiceResponse>> AddProduct(Product product)
        {
            if (product is null) return BadRequest("Model is null");

            var response = await productService.AddProduct(product);

            return Ok(response);
        }
    }
}
