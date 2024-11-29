namespace Domain.Entities
{
    public interface IProductDataService
    {
        decimal? GetPrice(Product product);
    }
}
