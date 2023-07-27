using App.Application.AccountOperations;
using App.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace App.API.Extensions;

/// <summary>
/// Provides extension methods for configuring and adding application services to the service collection.
/// </summary>
public static class ApplicationExtension
{
    /// <summary>
    /// Adds application services to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection instance to add the services to.</param>
    /// <param name="configuration">The IConfiguration instance for accessing the application configuration.</param>
    /// <returns>The updated IServiceCollection instance.</returns>
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add the UserContext to the service collection with the specified connection string.
        services.AddDbContext<UserContext>(opt =>
        {
            opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(typeof(Login.Handler).Assembly);
        });

        return services;
    }
}

