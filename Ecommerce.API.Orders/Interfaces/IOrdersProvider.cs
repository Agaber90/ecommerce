using ECommerce.Api.Orders.Models;

namespace Ecommerce.API.Orders.Interfaces;

public interface IOrdersProvider
{
    Task<(bool IsSuccess, IEnumerable<Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
}

