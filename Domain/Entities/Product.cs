using Domain.Interfaces;

namespace Domain.Entities
{
    public class Product : IProduct
    {
        public Product((string code, string name) product)
        {
            Code = product.code;
            Name = product.name;
        }
        public int Id { get; private set; }
        public string Code { get; private set; }
        public string Name { get; private set; }
    }
}