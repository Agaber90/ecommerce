using Ecommerce.API.Orders.Interfaces;
using ECommerce.Api.Orders.Providers;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Orders.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrdersProvider _ordersProvider;

    public OrdersController(IOrdersProvider ordersProvider)
    {
        _ordersProvider = ordersProvider;
    }

    [HttpGet("{customerId}/get-orders")]
    public async Task<IActionResult> GetOrdersAsync(int customerId)
    {
        var result = await _ordersProvider.GetOrdersAsync(customerId);
        if (result.IsSuccess)
        {
            return Ok(result.Orders);
        }
        return NotFound();
    }
}

