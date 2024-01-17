using Ecommerce.API.Products.Models;

namespace ECommerce.Api.Orders.Interfaces;
public interface IProductsProvider
{
    Task<(bool IsSuccess, IEnumerable<Product> products, string ErrorMessage)> GetProductsAsync();

    Task<(bool IsSuccess, Product product, string ErrorMessage)> GetProductAsync(int id);

}
