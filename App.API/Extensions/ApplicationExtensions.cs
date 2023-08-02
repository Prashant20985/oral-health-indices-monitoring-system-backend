﻿using App.Application.Behavior;
using App.Application.Interfaces;
using App.Domain.Repository;
using App.Infrastructure.Configuration;
using App.Infrastructure.ContextAccessor;
using App.Infrastructure.Email;
using App.Persistence.Contexts;
using App.Persistence.Repository;
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

        // 
        // Add a scoped dependency for IUserRepository with the impliementation of UserRepository.
        services.AddScoped<IUserRepository, UserRepository>();

        // Add MediatoR
        services.AddMediatR(cfg =>
        {
            // Registering services from Application layer assembly.
            cfg.RegisterServicesFromAssemblies(typeof(Application.AssemblyReference).Assembly);
            // Add logging pipeline behavior.
            cfg.AddOpenBehavior(typeof(LoggingBehaviorPipeline<,>));
        });

        // Add a singleton dependency for IEmailTemplatePathProvider with the implementation of EmailTemplatePathProvider.
        services.AddSingleton<IEmailTemplatePathProvider, EmailTemplatePathProvider>();

        // Configure and bind the EmailSettings section from the configuration.
        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));

        // Add a transient dependency for IEmailService with the implementation of EmailService.
        services.AddTransient<IEmailService, EmailService>();

        // Add a transient for IHttpContextAccessorService with implimentation of HttpContextAccessorService.
        services.AddTransient<IHttpContextAccessorService, HttpContextAccessorService>();

        return services;
    }
}

