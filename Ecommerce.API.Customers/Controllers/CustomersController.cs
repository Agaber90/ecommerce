using Ecommerce.API.Customers.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Customers.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly ICustomersProvider _customersProvider;

    public CustomersController(ICustomersProvider customersProvider)
    {
        _customersProvider = customersProvider;
    }

    [HttpGet("get-customers")]
    public async Task<IActionResult> GetCustomersAsync()
    {
        var result = await _customersProvider.GetCustomersAsync();
        if (result.IsSuccess)
        {
            return Ok(result.Customers);
        }
        return NotFound();
    }

    [HttpGet("{id}/get-customer")]
    public async Task<IActionResult> GetCustomerAsync(int id)
    {
        var result = await _customersProvider.GetCustomerAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result.Customer);
        }
        return NotFound();
    }
}

