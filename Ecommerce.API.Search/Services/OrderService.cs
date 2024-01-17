using Ecommerce.API.Search.Interfaces;
using Ecommerce.API.Search.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text.Json;

namespace Ecommerce.API.Search.Services;
public class OrderService : IOrderService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<OrderService> _logger;
    public OrderService(IHttpClientFactory httpClientFactory,
        ILogger<OrderService> logger)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<(bool IsSuccess, IEnumerable<Order> Orders, string Message)> GetOrderAsync(int customerId)
    {
        try
        {
            var client = _httpClientFactory.CreateClient("OrderService");
            var response = await client.GetAsync($"api/orders/{customerId}/get-orders");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions() { PropertyNameCaseInsensitive = true, };
                var result = JsonSerializer.Deserialize<IEnumerable<Order>>(content, options);

                return (true, result, null);
            }
            return (false, null, response.ReasonPhrase);
        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, null, ex.Message);
        }
    }
}
