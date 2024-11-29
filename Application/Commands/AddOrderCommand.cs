using Domain.Entities;
using Domain.Interfaces;
using Domain.ValueObjects;
using MediatR;

namespace Application.Commands
{
    public record AddOrderCommand((ICustomer customer, Address address,
            IEnumerable<(IProduct product, int quantity)> items) orderDto) : IRequest<int>;
}
