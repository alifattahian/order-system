using Domain.Constants;

namespace Domain.Entities
{
    public class ProductPrice
    {
        public ProductPrice((IProduct product, DateOnly validFromDate, DateOnly validUntilDate, decimal price) productPriceDto)
        {
            Product = productPriceDto.product;
            ValidFromDate = productPriceDto.validFromDate;
            ValidUntilDate = productPriceDto.validUntilDate;
            Price = productPriceDto.price;
            Status = ProductPriceStatus.Active;

        }
        public int Id { get; private set; }
        public IProduct Product { get; private set; }
        public ProductPriceStatus Status { get; set; }
        public DateOnly ValidFromDate { get; private set; }
        public DateOnly ValidUntilDate { get; private set; }
        public decimal Price { get; private set; }
        public decimal Profit { get; init; }
        public decimal NetPrice { get { return Price + Profit; } }

    }
}
