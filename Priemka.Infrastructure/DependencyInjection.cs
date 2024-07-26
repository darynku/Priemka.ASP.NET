using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Priemka.Application.DataAccess;
using Priemka.Domain.Interfaces;
using Priemka.Infrastructure.Options;
using Priemka.Infrastructure.Providers;
using Priemka.Infrastructure.Repositories;
using StackExchange.Redis;

namespace Priemka.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
      
        services.AddDbContext<ApplicationDbContext>(c => c.UseSqlServer(configuration.GetConnectionString("Default")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddScoped<IDoctorsRepository, DoctorsRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();


        services.AddScoped<IJwtProvider, JwtProvider>();

        //services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("localhost"));
        services.AddStackExchangeRedisCache(
            options =>
            {
                options.Configuration = configuration.GetConnectionString("Redis");
            });
        return services;
    }
}
