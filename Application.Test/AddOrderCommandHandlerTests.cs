using Application.Commands;
using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Test;
using Domain.ValueObjects;
using Moq;

namespace Application.Test
{
    public class AddOrderCommandHandlerTests
    {
        protected Address Address => new Address("Tehran", "123456789", "my address location description");
        protected Customer Customer { set; get; }
        private Mock<ITimeService> _timeServiceMock;
        [SetUp]
        public void Setup()
        {

            Customer = new Customer((name: "Anderson", Address));
            _timeServiceMock = new Mock<ITimeService>();
            var timeService = new TimeService();
            _timeServiceMock.Setup(timeService => timeService.GetLocalDateTime())
                .Returns(timeService.ConvertToLocalDateTime(TimeConstants.Utc9am));

            _timeServiceMock.Setup(timeService => timeService.ConvertToLocalDateTime(It.IsAny<DateTime>()))
                .Returns((DateTime datetime) => timeService.ConvertToLocalDateTime(datetime));
        }

        [Test]
        public void Test_addCommandValidators()
        {


            AddOrderCommand? addOrderCommand = new AddOrderCommand((
               customer: Customer,
               address: Address,
               items: new (IProduct product, int quantity)[]{
                         (product:EntitiesCollection. Product1,quantity:15),
                        (product: EntitiesCollection.Product2,quantity:10)
                   }
               ));

            var addOrderCommandHandler = new AddOrderCommandHandler(_timeServiceMock.Object);

            Assert.DoesNotThrowAsync(async () =>
            {
                await addOrderCommandHandler.Handle(addOrderCommand, default(CancellationToken));
            });

            AddOrderCommand? addOrderCommand2 = new AddOrderCommand((
              customer: Customer,
              address: Address,
              items: new (IProduct product, int quantity)[]{
                         (product:EntitiesCollection. Product1,quantity:1),
                        (product: EntitiesCollection.Product2,quantity:1)
                  }
              ));




            var exception = Assert.ThrowsAsync<FluentValidation.ValidationException>(async () =>
            {
                await addOrderCommandHandler.Handle(addOrderCommand2, default(CancellationToken));
            });


        }

    }
}
