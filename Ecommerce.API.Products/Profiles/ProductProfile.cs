namespace Ecommerce.API.Products.Profiles;
public class ProductProfile : AutoMapper.Profile
{
    public ProductProfile()
    {
        CreateMap<Db.Product, Models.Product>();
    }
}
