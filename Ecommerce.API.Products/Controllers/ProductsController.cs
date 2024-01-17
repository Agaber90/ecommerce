using ECommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Products.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductsProvider _productsProvider;

    public ProductsController(IProductsProvider productsProvider)
    {
        _productsProvider = productsProvider;
    }

    [HttpGet("get-products")]
    public async Task<IActionResult> GetProductsAsync()
    {
        var result = await _productsProvider.GetProductsAsync();
        if (result.IsSuccess)
        {
            return Ok(result.products);
        }
        return NotFound();
    }

    [HttpGet("{id}/get-product")]
    public async Task<IActionResult> GetProductAsync(int id)
    {
        var result = await _productsProvider.GetProductAsync(id);

        if (result.IsSuccess)
        {
            return Ok(result.product);
        }

        return NotFound();
    }
}
