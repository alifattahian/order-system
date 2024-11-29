using Domain.Entities;
using Moq;

namespace Domain.Test
{
    public class ProductTests
    {


        protected Mock<IProductDataService> _productDataServiceMock;
        [SetUp]
        public void Setup()
        {
            _productDataServiceMock = new Mock<IProductDataService>();
        }

        [Test]
        public void Test_CorrectProduct()
        {
            Product product = null;
            Assert.DoesNotThrow(() =>
            {
                product = new Product((code: "AB-X1", name: "Flower Pot"), _productDataServiceMock.Object);
            });

            Assert.AreEqual("AB-X1", product.Code, "product Code is not set!");
            Assert.AreEqual("Flower Pot", product.Name, "product Name is not set!");

        }

        [Test]
        public void Test_Product_Price_Invoke_service()
        {
            Product product = new Product((code: "AB-X1", name: "Flower Pot"), _productDataServiceMock.Object);
            _productDataServiceMock.Setup(productService => productService.GetPrice(product)).Returns(20000);

            var price = product.GetPrice();

            Assert.AreEqual(20000, price, "price is not expected!");
        }

    }
}
