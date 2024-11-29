using Domain.Constants;

namespace Domain.Entities
{
    public sealed class OrderItem
    {
        public OrderItem((Order order, IProduct product, int quantity) orderItem)
        {
            Order = orderItem.order;
            Product = orderItem.product;
            Quantity = orderItem.quantity;
            var price = Product.GetPrice();
            if (price.GetValueOrDefault() == 0)
            {
                throw new Domain.Exceptions.DomainValidationException(ErrorCodes.ProductShouldHavePriceForSales);
            }
            Price = price.GetValueOrDefault();
            TotalAmount = Price * Quantity;

        }
        public int Id { get; private set; }
        public Order Order { get; private set; }
        public int OderId { get; private set; }


        public IProduct Product { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
        public decimal TotalAmount { get; private set; }
    }
}
