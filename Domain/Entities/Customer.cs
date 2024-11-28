using Domain.Interfaces;
using Domain.ValueObjects;

namespace Domain;

public class Customer:ICustomer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Address Address { get; set; }

}
