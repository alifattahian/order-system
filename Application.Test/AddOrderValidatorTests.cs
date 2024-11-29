using Application.Validators;
using Domain;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Test;
using Domain.ValueObjects;
using FluentValidation;

namespace Application.Test
{
    public class AddOrderValidatorTests
    {
        protected Address Address => new Address("Tehran", "123456789", "my address location description");
        protected Customer Customer { set; get; }
        [SetUp]
        public void Setup()
        {

            Customer = new Customer((name: "Anderson", Address));
        }

        [Test]
        public void Test_netPrice()
        {
            var today9am = DateTime.Today.AddHours(9);
            AddOrderValidator addOrderValidator = new AddOrderValidator(new TimeService());
            Order? order = new Order((
               customer: Customer,
               address: Address,
               items: new (IProduct product, int quantity)[]{
                         (product:EntitiesCollection. Product1,quantity:15),
                        (product: EntitiesCollection.Product2,quantity:10)
                   }
               ))
            { CreateDateTime = today9am };
            Order? order2 = new Order((
              customer: Customer,
              address: Address,
              items: new (IProduct product, int quantity)[]{
                         (product:EntitiesCollection. Product1,quantity:1),
                        (product: EntitiesCollection.Product2,quantity:1)
                  }
              ))
            { CreateDateTime = today9am };


            Assert.DoesNotThrow(() => addOrderValidator.ValidateAndThrow(order));

            var exception = Assert.Throws<FluentValidation.ValidationException>(() => addOrderValidator.ValidateAndThrow(order2));


        }

        [Test]
        public void Test_TimeRange()
        {


            AddOrderValidator addOrderValidator = new AddOrderValidator(new TimeService());
            Order? order = new Order((
               customer: Customer,
               address: Address,
               items: new (IProduct product, int quantity)[]{
                         (product:EntitiesCollection. Product1,quantity:15),
                        (product: EntitiesCollection.Product2,quantity:10)
                   }
               ))
            { CreateDateTime = TimeConstants.Utc9am };
            Order? order2 = new Order((
              customer: Customer,
              address: Address,
              items: new (IProduct product, int quantity)[]{
                         (product:EntitiesCollection. Product1,quantity:15),
                        (product: EntitiesCollection.Product2,quantity:10)
                  }
              ))
            { CreateDateTime = TimeConstants.Utc5pm };

            Assert.DoesNotThrow(() => addOrderValidator.ValidateAndThrow(order));

            var exception = Assert.Throws<FluentValidation.ValidationException>(() => addOrderValidator.ValidateAndThrow(order2));


        }
    }
}