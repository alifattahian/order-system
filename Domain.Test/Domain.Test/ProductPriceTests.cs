using Domain.Entities;

namespace Domain.Test
{
    public class ProductPriceTests
    {

        [SetUp]
        public void Setup()
        {


        }

        [Test]
        public void Test_CorrectProductPrice()
        {
            ProductPrice productPrice = null;
            Assert.DoesNotThrow(() =>
            {
                productPrice = new ProductPrice(
                    (product: EntitiesCollection.Product1,
                    validFromDate: DateOnly.FromDateTime(DateTime.Now),
                    validUntilDate: DateOnly.FromDateTime(DateTime.Now.AddDays(20)),
                    price: 20000))
                { Profit = 1000 };
            });



            Assert.AreEqual(EntitiesCollection.Product1, productPrice.Product, "product is not set!");
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now), productPrice.ValidFromDate, "ValidFromDate is not set!");
            Assert.AreEqual(DateOnly.FromDateTime(DateTime.Now.AddDays(20)), productPrice.ValidUntilDate, "ValidUntilDate is not set!");
            Assert.AreEqual(20000, productPrice.Price, "product price is not set!");

        }
    }
}
