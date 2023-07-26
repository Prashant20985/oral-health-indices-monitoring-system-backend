using App.Application.Interfaces;
using App.Domain.Models.Users;
using App.Infrastructure.Configuration;
using App.Infrastructure.Token;
using App.Persistence.Contexts;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App.API.Extensions;

/// <summary>
/// Provides extension methods for configuring and adding identity services to the service collection.
/// </summary>
public static class IdentityExtension
{
    /// <summary>
    /// Adds identity services to the service collection.
    /// </summary>
    /// <param name="services">The IServiceCollection instance to add the services to.</param>
    /// <param name="config">The IConfiguration instance for accessing the application configuration.</param>
    /// <returns>The updated IServiceCollection instance.</returns>
    public static IServiceCollection AddIdentityServices(this IServiceCollection services,
        IConfiguration config)
    {
        // Configure the options for the DataProtectionTokenProvider.
        services.Configure<DataProtectionTokenProviderOptions>(opts =>
        {
            opts.TokenLifespan = TimeSpan.FromHours(10);
        });

        // Add Identity with the specified configuration for AppUser and IdentityRole.
        services.AddIdentity<User, IdentityRole>(opt =>
        {
            opt.Password.RequiredLength = 8;
            opt.Password.RequireDigit = true;
            opt.Password.RequireLowercase = true;
            opt.Password.RequireUppercase = true;
            opt.Password.RequireNonAlphanumeric = true;
            opt.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<UserContext>()
            .AddDefaultTokenProviders();

        services.AddScoped<UserManager<User>>();

        // Configure JwtConfig options from the configuration.
        services.Configure<JwtConfig>(config.GetSection("JwtConfig"));

        // Create a symmetric security key from the secret key specified in the configuration.
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(config["JwtConfig:secretKey"]));

        var jwtConfig = config.GetSection("JwtConfig").Get<JwtConfig>();

        // Configure token validation parameters.
        var tokenValidationParams = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = key,
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            RequireExpirationTime = true,
            ClockSkew = TimeSpan.Zero
        };

        // Add authentication services with JwtBearer scheme and configure it with token validation parameters.
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParams;
            });

        services.AddSingleton(tokenValidationParams);

        // Add a transient dependency for ITokenService with the implementation of TokenService.
        services.AddTransient<ITokenService, TokenService>();

        return services;
    }
}