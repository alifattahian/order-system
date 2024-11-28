using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Test
{
    public class OrderTests
    {
        public List<Product> Products { get; set; }

        protected Product Product1 => Products.Where(x => x.Code == "AB-X1").FirstOrDefault();
        protected Product Product2 => Products.Where(x => x.Code == "SC-91").FirstOrDefault();
        protected Address Address => new Address("Tehran", "123456789", "my address location description");
        protected Customer Customer { set; get; }
        [SetUp]
        public void Setup()
        {
            Products = new List<Product>(new[]
            {
                new Product((code:"AB-X1",name: "Flower Pot")),
                new Product((code:"SC-91",name: "Lamp"))
            });
            Customer = new Customer();
        }

        [Test]
        public void Test_CorrectOrder()
        {
            Order order = null;
            Assert.DoesNotThrow(() =>
            {
                order = new Order((
                   customer: Customer,
                   address: Address,
                   items: new (IProduct product, int quantity)[]{
                         (product: Product1,quantity:15),
                        (product: Product2,quantity:10)
                       }
                   ));
            }, "order should correctly be created!");

            Assert.AreEqual(Customer, order.Customer, "customer is not set");
            Assert.AreEqual(Address, order.Address, "address is not set");
            Assert.AreEqual(2, order.Items.Count, "count of items is not correct!");
            foreach (var item in order.Items)
            {
                Assert.AreEqual(order, item.Order, "order in orderItem is not set!");
            }

            Assert.IsTrue(order.Items.Any(item => item.Product == Product1 && item.Quantity == 15), "product or quantity is not set!");
            Assert.IsTrue(order.Items.Any(item => item.Product == Product2 && item.Quantity == 10), "product or quantity is not set!");
        }

        [Test]
        public void Throw_Error_If_Customer_Is_Not_set()
        {
            Order order = null;
            var exception = Assert.Throws<DomainValidationException>(() =>
            {
                order = new Order((
                   customer: null,
                   address: Address,
                   items: new (IProduct product, int quantity)[]{
                         (product: Product1,quantity:15),
                        (product: Product2,quantity:10)
                       }
                   ));
            }, "should throw an Exception when customer is not set!");

            Assert.AreEqual(ErrorCodes.CustomerNotEmpty.Code, exception.Code);
        }

        [Test]
        public void Order_Should_Have_Atleast_one_Item()
        {
            Order order = null;
            var exception = Assert.Throws<DomainValidationException>(() =>
            {
                order = new Order((
                   customer: new Customer(),
                   address: Address,
                   items: new (IProduct product, int quantity)[]{

                       }
                   ));
            }, "should throw an Exception when no item is added to order!");

            Assert.AreEqual(ErrorCodes.NoItemIsAddedToOrder.Code, exception.Code);
        }
    }
}