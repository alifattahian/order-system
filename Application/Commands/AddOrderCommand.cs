using Application.Dto;
using MediatR;

namespace Application.Commands;

public record AddOrderCommand((int customerId, AddressDto address,
        IEnumerable<(string productCode, int quantity)> items) orderDto) : IRequest<int>;
