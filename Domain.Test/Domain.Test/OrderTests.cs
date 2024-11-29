using Domain.Constants;
using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;

namespace Domain.Test;

public class OrderTests
{



    protected Address Address => new Address("Tehran", "123456789", "my address location description");
    protected Customer Customer { set; get; }
    [SetUp]
    public void Setup()
    {

        Customer = new Customer((name: "Anderson", Address));
    }

    [Test]
    public void Test_CorrectOrder()
    {
        Order? order = null;
        Assert.DoesNotThrow(() =>
        {
            order = new Order((
               customer: Customer,
               address: Address,
               items: new (IProduct product, int quantity)[]{
                         (product:EntitiesCollection. Product1,quantity:15),
                        (product: EntitiesCollection.Product2,quantity:10)
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

        //############ item #1 ##########
        var item1 = order.Items.FirstOrDefault(item => item.Product == EntitiesCollection.Product1);

        Assert.AreEqual(15, item1.Quantity, " quantity is not set!");
        Assert.AreEqual(21000, item1.Price, "price is not correct");
        Assert.AreEqual(315000, item1.TotalAmount, "total amount of item1 is not correct!");

        //############ item #2 ##########
        var item2 = order.Items.FirstOrDefault(item => item.Product == EntitiesCollection.Product2);

        Assert.AreEqual(10, item2.Quantity, "quantity is not set!");
        Assert.AreEqual(5500, item2.Price, "price is not correct");
        Assert.AreEqual(55000, item2.TotalAmount, "total amount of item1 is not correct!");

        Assert.AreEqual(370000, order.TotalAmount);
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
                         (product:EntitiesCollection. Product1,quantity:15),
                        (product: EntitiesCollection.Product2,quantity:10)
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
               customer: Customer,
               address: Address,
               items: new (IProduct product, int quantity)[]{

                   }
               ));
        }, "should throw an Exception when no item is added to order!");

        Assert.AreEqual(ErrorCodes.NoItemIsAddedToOrder.Code, exception.Code);
    }
    [Test]
    public void Test_Discount()
    {
        Order? order = new Order((
               customer: Customer,
               address: Address,
               items: new (IProduct product, int quantity)[]{
                         (product:EntitiesCollection. Product1,quantity:15),
                        (product: EntitiesCollection.Product2,quantity:10)
                   }
               ));
        order.SetDiscount(10000);
        Assert.AreEqual(10000, order.DiscountAmount);
        Assert.AreEqual(360000, order.NetPrice);

        order.SetDiscountByPercent(10);
        Assert.AreEqual(37000, order.DiscountAmount);
        Assert.AreEqual(333000, order.NetPrice);

    }
}


