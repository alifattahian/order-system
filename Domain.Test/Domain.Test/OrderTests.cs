using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain.Test
{
    public class OrderTests
    {
        public List<Product> Products { get; set; }
        [SetUp]
        public void Setup()
        {
            Products = new List<Product>(new[]
            {
                new Product((code:"AB-X1",name: "Flower Pot")),
                new Product((code:"SC-91",name: "Lamp"))
            });
        }

        [Test]
        public void Test_CorrectOrder()
        {
            var product1 = Products.Where(x => x.Code == "AB-X1").FirstOrDefault();
            var product2 = Products.Where(x => x.Code == "SC-91").FirstOrDefault();
            var customer = new Customer();
            var address = new Address("Tehran", "123456789", "my address location description");
            Order order = null;
            Assert.DoesNotThrow(() =>
            {
                order = new Order((
                   customer: customer,
                   address: address,
                   items: new (IProduct product, int quantity)[]{
                         (product: product1,quantity:15),
                        (product: product2,quantity:10)
                       }
                   ));
            }, "order should correctly be created!");

            Assert.AreEqual(customer, order.Customer, "customer is not set");
            Assert.AreEqual(address, order.Address, "address is not set");
            Assert.AreEqual(2, order.Items.Count, "count of items is not correct!");
            foreach (var item in order.Items)
            {
                Assert.AreEqual(order, item.Order, "order in orderItem is not set!");
            }



            Assert.IsTrue(order.Items.Any(item => item.Product == product1 && item.Quantity == 15), "product or quantity is not set!");
            Assert.IsTrue(order.Items.Any(item => item.Product == product2 && item.Quantity == 10), "product or quantity is not set!");
        }

        [Test]
        public void Throw_Error_If_Customer_Is_Not_set()
        {
            var product1 = Products.Where(x => x.Code == "AB-X1").FirstOrDefault();
            var product2 = Products.Where(x => x.Code == "SC-91").FirstOrDefault();

            var address = new Address("Tehran", "123456789", "my address location description");
            Order order = null;
            var exception = Assert.Throws<DomainValidationException>(() =>
            {
                order = new Order((
                   customer: null,
                   address: address,
                   items: new (IProduct product, int quantity)[]{
                         (product: product1,quantity:15),
                        (product: product2,quantity:10)
                       }
                   ));
            }, "should throw DomainValidationException when customer is not set!");

            Assert.AreEqual("customer is not set!", exception.Message);
        }
    }
}