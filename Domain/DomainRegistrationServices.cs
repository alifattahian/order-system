using Domain.Entities;
using Domain.Interfaces;

namespace Domain
{
    public static class DomainRegistrationServices
    {
        public static IEnumerable<(Type interfaceType, Type implementationType)> GetServices()
        {
            return new (Type interfaceType, Type implementationType)[]
            {
                (interfaceType:typeof(IProductDataService),implementationType:typeof(ProductDataService)),
                (interfaceType:typeof(ITimeService),implementationType:typeof(TimeService))
            };
        }
    }
}
