using Domain.Entities;
using Domain.Interfaces;

namespace Repository.Repositories;

internal class ProductRepository : Repository<Product>, IProductRepository
{
    public Product GetProductByCode(string code)
    {
        throw new NotImplementedException();
    }
}
