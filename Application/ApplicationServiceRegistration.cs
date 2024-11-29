using Application.Commands;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {

            var applicationAssembly = typeof(AddOrderCommand).Assembly;
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(applicationAssembly));
            foreach (var registerItem in DomainRegistrationServices.GetServices())
            {
                services.AddTransient(registerItem.interfaceType, registerItem.implementationType);
            }

            return services;

        }
    }
}
