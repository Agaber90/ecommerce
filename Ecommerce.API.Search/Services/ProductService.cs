using Ecommerce.API.Search.Interfaces;
using Ecommerce.API.Search.Models;
using System.Text.Json;

namespace Ecommerce.API.Search.Services;
public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger<ProductService> _logger;

    public ProductService(IHttpClientFactory httpClientFactory,
        ILogger<ProductService> logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync()
    {
        try
        {
            var client = _httpClientFactory.CreateClient("ProductService");
            var response = await client.GetAsync("api/Products/get-products");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsByteArrayAsync();
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
                var result = JsonSerializer.Deserialize<IEnumerable<Product>>(content, options);
                return (true, result, null);
            }
            return (false, Enumerable.Empty<Product>(), response.ReasonPhrase);

        }
        catch (Exception ex)
        {
            _logger?.LogError(ex.ToString());
            return (false, Enumerable.Empty<Product>(), ex.Message);
        }
    }
}
