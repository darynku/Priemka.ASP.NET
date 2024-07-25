using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Priemka.Application.Validator;
namespace Priemka.Application
{
    public static class DependecyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddMediatR(c => c.RegisterServicesFromAssemblies(assemblies));



            services.Scan(scan => 
            scan.FromAssemblies(assemblies)
            .AddClasses(classes => classes.AssignableTo(typeof(IValidator<>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());

            return services;
        }
    }
}
