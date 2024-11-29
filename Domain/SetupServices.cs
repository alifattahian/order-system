using Domain.Entities;

namespace Domain
{
    public class SetupServices
    {
        public IEnumerable<(Type interfaceType, Type implementationType)> GetServices()
        {
            return new (Type interfaceType, Type implementationType)[]
            {
                (interfaceType:typeof(IProductDataService),implementationType:typeof(ProductDataService))
            };
        }
    }
}
