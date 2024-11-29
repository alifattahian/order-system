using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain;

public class Customer : ICustomer
{
    public Customer((string name, Address address) customerDto)
    {
        Name = customerDto.name;
        Address = customerDto.address;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }

}
