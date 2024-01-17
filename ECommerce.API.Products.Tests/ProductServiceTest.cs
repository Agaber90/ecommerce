using AutoMapper;
using Ecommerce.API.Products.Db;
using Ecommerce.API.Products.Profiles;
using Ecommerce.API.Products.Providers;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Products.Tests;
public class ProductServiceTest
{
    [Fact]
    public async Task GetProductsReturnsAllProducts()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductsReturnsAllProducts))
            .Options;

        var dbContext = new ProductsDbContext(options);
        CreateProducts(dbContext);

        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        var mapper = new Mapper(configuration);

        var productProvider = new ProductsProvider(dbContext, null, mapper);
        var products = await productProvider.GetProductsAsync();

        Assert.True(products.IsSuccess);
        Assert.True(products.products.Any());
        Assert.Null(products.ErrorMessage);
    }

    [Fact]
    public async Task GetProductsReturnsAllProductsUsingValidId()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductsReturnsAllProductsUsingValidId))
            .Options;

        var dbContext = new ProductsDbContext(options);
        CreateProducts(dbContext);

        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        var mapper = new Mapper(configuration);

        var productProvider = new ProductsProvider(dbContext, null, mapper);
        var product = await productProvider.GetProductAsync(1);

        Assert.True(product.IsSuccess);
        Assert.NotNull(product.product);
        Assert.True(product.product.Id == 1);
        Assert.Null(product.ErrorMessage);
    }

    [Fact]
    public async Task GetProductsReturnsAllProductsUsingInValidId()
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseInMemoryDatabase(nameof(GetProductsReturnsAllProductsUsingValidId))
            .Options;

        var dbContext = new ProductsDbContext(options);
        CreateProducts(dbContext);

        var productProfile = new ProductProfile();
        var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
        var mapper = new Mapper(configuration);

        var productProvider = new ProductsProvider(dbContext, null, mapper);
        var product = await productProvider.GetProductAsync(-1);

        Assert.False(product.IsSuccess);
        Assert.Null(product.product);
        Assert.NotNull(product.ErrorMessage);
    }

    private void CreateProducts(ProductsDbContext dbContext)
    {
        for (int i = 1; i < 10; i++)
        {
            dbContext.Products.Add(new Product
            {
                Id = i,
                Name = Guid.NewGuid().ToString(),
                Inventory = i + 10,
                Price = (decimal)(i * 3.14),
            });
        }
        dbContext.SaveChanges();
    }
}