using Domain.Entities;
using Domain.Interfaces;

namespace Repository.Repositories;

internal class CustomerRepository : Repository<Customer>, ICustomerRepository
{
}
