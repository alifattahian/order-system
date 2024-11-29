using Domain.Entities;
using Domain.Interfaces;
using Moq;

namespace Domain.Test
{
    internal class ProductDataServiceTests
    {
        [Test]
        public void Test_check_if_get_price()
        {
            var priceRepositoryMock = new Mock<IProductPriceRepository>();
            var productDataService = new ProductDataService(priceRepositoryMock.Object);
            priceRepositoryMock.Setup(repository => repository.GetAll())
                .Returns(EntitiesCollection.Prices);

            var price = productDataService.GetPrice(EntitiesCollection.Product1);
            Assert.AreEqual(21000, price, "Price of product1() should be 21000");

        }
    }
}
