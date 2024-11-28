using Domain.Entities;

namespace Domain.Test
{
    public class ProductTests
    {
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void Test_CorrectOrder()
        {
            Product product = null;
            Assert.DoesNotThrow(() =>
            {
                product = new Product((code: "AB-X1", name: "Flower Pot"));
            });

            Assert.AreEqual("AB-X1", product.Code, "product Code is not set!");
            Assert.AreEqual("Flower Pot", product.Name, "product Name is not set!");

        }
    }
}
