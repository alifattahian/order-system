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
        public AddOrderCommandHandler(ITimeService timeService)
        {
            this.timeService = timeService;
        }
        public async Task<int> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var order = new Order(request.orderDto)
            {
                CreateDateTime = timeService.GetLocalDateTime(),
            };
            var addOrderValidator = new AddOrderValidator(timeService);
            addOrderValidator.ValidateAndThrow(order);
            return order.Id;
        }
    }
}
