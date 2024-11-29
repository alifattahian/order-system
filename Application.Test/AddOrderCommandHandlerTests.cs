using Application.Commands;
using Application.Dto;
using Domain.Interfaces;
using Domain.Test;
using Moq;

namespace Application.Test
{
    public class AddOrderCommandHandlerTests
    {
        protected AddressDto Address => new AddressDto("Tehran", "123456789", "my address location description");

        private Mock<ITimeService> _timeServiceMock;
        private Mock<IProductRepository> productRepositoryMock;
        private Mock<ICustomerRepository> customerRepositoryMock;
        [SetUp]
        public void Setup()
        {

            _timeServiceMock = new Mock<ITimeService>();

            productRepositoryMock = new Mock<IProductRepository>();
            customerRepositoryMock = new Mock<ICustomerRepository>();

            var timeService = new TimeService();
            _timeServiceMock.Setup(timeService => timeService.GetLocalDateTime())
                .Returns(timeService.ConvertToLocalDateTime(TimeConstants.Utc9am));

            _timeServiceMock.Setup(timeService => timeService.ConvertToLocalDateTime(It.IsAny<DateTime>()))
                .Returns((DateTime datetime) => timeService.ConvertToLocalDateTime(datetime));

            customerRepositoryMock
                .Setup(service => service.GetById(1)).Returns(EntitiesCollection.Customer1);
            productRepositoryMock
                .Setup(service => service.GetProductByCode(EntitiesCollection.Product1.Code))
                .Returns(EntitiesCollection.Product1);
            productRepositoryMock
                .Setup(service => service.GetProductByCode(EntitiesCollection.Product2.Code))
                .Returns(EntitiesCollection.Product2);

        }

        [Test]
        public void Test_addCommandValidators()
        {


            AddOrderCommand? addOrderCommand = new AddOrderCommand((
               customerId: 1,
               address: Address,
               items: new (string productCode, int quantity)[]{
                         (productCode:EntitiesCollection. Product1.Code,quantity:15),
                        (productCode: EntitiesCollection.Product2.Code,quantity:10)
                   }
               ));

            var addOrderCommandHandler = new AddOrderCommandHandler(_timeServiceMock.Object,
                customerRepositoryMock.Object,
                productRepositoryMock.Object);

            Assert.DoesNotThrowAsync(async () =>
            {
                await addOrderCommandHandler.Handle(addOrderCommand, default(CancellationToken));
            });

            AddOrderCommand? addOrderCommand2 = new AddOrderCommand((
              customerId: 1,
              address: Address,
              items: new (string productCode, int quantity)[]{
                         (productCode:EntitiesCollection. Product1.Code,quantity:1),
                        (productCode: EntitiesCollection.Product2.Code,quantity:1)
                  }
              ));




            var exception = Assert.ThrowsAsync<FluentValidation.ValidationException>(async () =>
            {
                await addOrderCommandHandler.Handle(addOrderCommand2, default(CancellationToken));
            });


        }

    }
}
