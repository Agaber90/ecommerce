using Ecommerce.API.Search.Models;

namespace Ecommerce.API.Search.Interfaces;
public interface IProductService
{
    Task<(bool IsSuccess, IEnumerable<Product> Products, string ErrorMessage)> GetProductsAsync();
}
