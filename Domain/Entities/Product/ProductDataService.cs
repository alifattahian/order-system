using Domain.Interfaces;

namespace Domain.Entities
{
    internal class ProductDataService : IProductDataService
    {
        private readonly IProductPriceRepository _priceRepository;
        public ProductDataService(IProductPriceRepository productPriceRepository)
        {
            _priceRepository = productPriceRepository;
        }
        public decimal? GetPrice(Product product)
        {
            var productPrices = _priceRepository.GetAll();
            DateOnly today = DateOnly.FromDateTime(DateTime.Now);
            var productPrice = productPrices.FirstOrDefault(
                x => x.Product.Equals(product) &&
                x.Status == Constants.ProductPriceStatus.Active &&
                x.ValidFromDate.CompareTo(today) <= 0 &&
                x.ValidUntilDate.CompareTo(today) >= 0);

            if (productPrice == null) return null;
            return productPrice.NetPrice;
        }
    }
}
