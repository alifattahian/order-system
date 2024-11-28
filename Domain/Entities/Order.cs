using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Order
    {
        public Order((ICustomer customer, Address address,
            IEnumerable<( IProduct product, int quantity)> items) orderDto)
        {
            Customer = orderDto.customer;
            Address = orderDto.address;
            Items= new List<OrderItem>();
            foreach (var item in orderDto.items)
            {
                Items.Add(new OrderItem((order: this, product: item.product, quantity: item.quantity)));
            }

        }
        public int Id { get; private set; }
        public ICustomer Customer { get; private set; }
        public Address Address { get; private set; }

        public List<OrderItem> Items { get; private set; }

    }
}
