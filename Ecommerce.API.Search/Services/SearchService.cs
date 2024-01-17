using Ecommerce.API.Search.Interfaces;

namespace Ecommerce.API.Search.Services;

public class SearchService : ISearchService
{
    private readonly IOrderService _orderService;
    private readonly IProductService _productService;
    private readonly ICustomersService _customersService;
    public SearchService(IOrderService orderService,
        IProductService productService,
        ICustomersService customersService)
    {
        _orderService = orderService;
        _productService = productService;
        _customersService = customersService;
    }


    public async Task<(bool IsSuccess, dynamic SearchResults)> SearchAsync(int customerId)
    {
        var ordersResult = await _orderService.GetOrderAsync(customerId);
        var productsResult = await _productService.GetProductsAsync();
        var customersResult = await _customersService.GetCustomerAsync(customerId);

        if (ordersResult.IsSuccess)
        {
            foreach (var orders in ordersResult.Orders)
            {
                foreach (var item in orders.Items)
                {
                    item.ProductName = productsResult.IsSuccess ?
                        productsResult.Products.FirstOrDefault(p => p.Id == item.ProductId)?.Name :
                        "Product information is not available";
                }
            }
            var result = new
            {
                Customer = customersResult.IsSuccess ?
                                customersResult.Customer :
                                new { Name = "Customer information is not available" },
                Orders = ordersResult.Orders,
            };
            return (true, result);
        }
        return (false, null);
    }
}

