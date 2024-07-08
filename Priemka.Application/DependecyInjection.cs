using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace Priemka.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddMediatR(c => c.RegisterServicesFromAssemblies(assemblies));

            
            return services;
        }
    }
}
