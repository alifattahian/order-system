using Domain.Constants;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Order
    {
        public Order((ICustomer customer, Address address,
            IEnumerable<(IProduct product, int quantity)> items) orderDto)
        {
            if (orderDto.customer == null)
            {
                throw new DomainValidationException(ErrorCodes.CustomerNotEmpty);
            }
            if (orderDto.items == null || !orderDto.items.Any())
            {
                throw new DomainValidationException(ErrorCodes.NoItemIsAddedToOrder);
            }
            Customer = orderDto.customer;
            Address = orderDto.address;
            Items = new List<OrderItem>();
            foreach (var item in orderDto.items)
            {
                Items.Add(new OrderItem((order: this, product: item.product, quantity: item.quantity)));
            }
            TotalAmount = Items.Sum(item => item.TotalAmount);
            CalculateNetPrice();

        }

        public int Id { get; private set; }
        public ICustomer Customer { get; private set; }
        public Address Address { get; private set; }

        public DateTime CreateDateTime { init; get; }

        public List<OrderItem> Items { get; private set; }

        public decimal DiscountAmount { get; private set; }
        public decimal DiscountPercent { get; private set; }
        public decimal TotalAmount { get; private set; }

        public void SetDiscount(decimal discountAmount)
        {
            DiscountAmount = discountAmount;
            DiscountPercent = 0;
            CalculateNetPrice();
        }

        public void SetDiscountByPercent(decimal discountPercent)
        {
            if (discountPercent < 0 || discountPercent > 100) throw new DomainValidationException(ErrorCodes.InvalidRange);
            DiscountPercent = discountPercent;
            DiscountAmount = TotalAmount * discountPercent / 100;
            CalculateNetPrice();

        }

        public decimal NetPrice
        {
            private set; get;
        }

        private void CalculateNetPrice()
        {
            NetPrice = TotalAmount - DiscountAmount;

        }

    }
}
