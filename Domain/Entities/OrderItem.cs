namespace Domain.Entities
{
    public class OrderItem
    {
        public OrderItem((Order order, IProduct product, int quantity) orderItem)
        {
            Order = orderItem.order;
            Product = orderItem.product;
            Quantity = orderItem.quantity;

        }
        public int Id { get; private set; }
        public Order Order { get; private set; }
        public int OderId { get; private set; }
        public int Quantity { get; private set; }
        public IProduct Product { get; private set; }
    }
}
