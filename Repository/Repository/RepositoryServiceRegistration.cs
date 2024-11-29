using Domain.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Repository.Repositories;

namespace Repository
{
    public static class RepositoryServiceRegistration
    {
        public static IServiceCollection RegisterRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<IProductPriceRepository, ProductPriceRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            return services;
        }
    }
}
