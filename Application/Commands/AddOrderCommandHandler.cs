using Application.Validators;
using Domain.Entities;
using Domain.Interfaces;
using FluentValidation;
using MediatR;

namespace Application.Commands
{
    internal class AddOrderCommandHandler : IRequestHandler<AddOrderCommand, int>
    {
        private readonly ITimeService timeService;
        private readonly ICustomerRepository customerRepository;
        private readonly IProductRepository productRepository;
        public AddOrderCommandHandler(ITimeService timeService,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            this.timeService = timeService;
            this.customerRepository = customerRepository;
            this.productRepository = productRepository;
        }

        public static Order ConvertAddOrderCommand(AddOrderCommand command, DateTime createDateTime,
            ICustomerRepository customerRepository,
            IProductRepository productRepository)
        {
            var addressDto = command.orderDto.address;
            ICustomer customer = customerRepository.GetById(command.orderDto.customerId);

            var order = new Order((
                customer: customer,
                address: new Domain.ValueObjects.Address(
            addressDto.city,
                    addressDto.zipCode,
                    addressDto.addressDescription),
                items: command.orderDto.items.Select(item =>
                {
                    (IProduct product, int quantity) newItem =
                        (product: productRepository.GetProductByCode(item.productCode),
                        quantity: item.quantity);
                    return newItem;
                })
                ))
            {
                CreateDateTime = createDateTime,
            };
            return order;
        }

        public async Task<int> Handle(AddOrderCommand command, CancellationToken cancellationToken)
        {
            var addressDto = command.orderDto.address;
            ICustomer customer = customerRepository.GetById(command.orderDto.customerId);

            var order = ConvertAddOrderCommand(command, timeService.GetLocalDateTime(),
                customerRepository, productRepository);
            var addOrderValidator = new AddOrderValidator(timeService);
            addOrderValidator.ValidateAndThrow(order);
            return order.Id;
        }
    }
}
