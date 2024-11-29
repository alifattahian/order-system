using Domain.Interfaces;

namespace Repository.Repositories
{
    internal class Repository<TEntity> : IRepository<TEntity>
    {
        public IEnumerable<TEntity> GetAll()
        {
            throw new NotImplementedException();
        }

        public TEntity GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
