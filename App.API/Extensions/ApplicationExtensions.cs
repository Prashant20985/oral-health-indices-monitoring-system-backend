using App.API.LogServices;
using App.Application.Behavior;
using App.Application.Core;
using App.Application.Interfaces;
using App.Domain.Models.Logs;
using App.Domain.Repository;
using App.Domain.UnitOfWork;
using App.Infrastructure.Configuration;
using App.Infrastructure.ContextAccessor;
using App.Infrastructure.Email;
using App.Infrastructure.GeneratePassword;
using App.Infrastructure.QueryFilter;
using App.Infrastructure.ReadCsv;
using App.Persistence.Contexts;
using App.Persistence.Repository;
using App.Persistence.UnitOfWorkImpl;
using FluentValidation;
using FluentValidation.AspNetCore;
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
        services.AddDbContext<OralEhrContext>(opt =>
        {
            opt.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        // Add CORS
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy", policy =>
            {
                policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithExposedHeaders("WWW-Authenticate")
                    .WithOrigins("http://localhost:3000", "https://localhost:3000");
            });
        });

        // Add a scoped dependency for IUserRepository with the impliementation of UserRepository.
        services.AddScoped<IUserRepository, UserRepository>();

        // Add MediatoR
        services.AddMediatR(cfg =>
        {
            // Registering services from Application layer assembly.
            cfg.RegisterServicesFromAssemblies(typeof(Application.AssemblyReference).Assembly);
            // Add logging pipeline behavior.
            cfg.AddOpenBehavior(typeof(LoggingBehaviorPipeline<,>));
            // Add validation pipeline behavior.
            cfg.AddOpenBehavior(typeof(ValidationBehaviorPipeline<,>));
            // Add unitOfWork pipeline behavior.
            cfg.AddOpenBehavior(typeof(UnitOfWorkBehaviorPipeline<,>));
        });

        // Add FluentValidation auto-validation
        services.AddFluentValidationAutoValidation();

        // Add validators from the specified assembly
        services.AddValidatorsFromAssembly(typeof(Application.AssemblyReference).Assembly);

        // Add AutoMapper with mapping profiles from the specified assembly
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // Add a scoped dependency for IGeneratePassword with the implementation of GeneratePassword.
        services.AddScoped<IGeneratePassword, GeneratePassword>();

        // Add a scoped dependency for IQueryFilter with the implementation of QueryFilter.
        services.AddScoped<IQueryFilter, QueryFilter>();

        // Add a scoped dependency for IUserRequestRepository with the implementation of UserRequestRepository.
        services.AddScoped<IUserRequestRepository, UserRequestRepository>();

        // Add a scoped dependency for IReadCsv with the implementation of ReadCsv.
        services.AddScoped<IReadCsv, ReadCsv>();

        // Add a scoped dependency for IUserContextUnitOfWork with the implementation of UserContextUnitOfWork.
        services.AddScoped<IUnitOfWork, OralEhrContextUnitOfWork>();

        // Add a scoped dependency for IGroupRepository with the implementation of GroupRepository.
        services.AddScoped<IGroupRepository, GroupRepository>();

        // Add a singleton dependency for IEmailTemplatePathProvider with the implementation of EmailTemplatePathProvider.
        services.AddSingleton<IEmailTemplatePathProvider, EmailTemplatePathProvider>();

        // Add a scoped dependency for IPatientRepository with the implementation of PatientRepository.
        services.AddScoped<IPatientRepository, PatientRepository>();

        // Add a scoped dependency for IResearchGroupRepository with the implementation of ResearchGroupRepository.
        services.AddScoped<IResearchGroupRepository, ResearchGroupRepository>();

        services.AddScoped<IStudentExamRepository, StudentExamRepository>();

        // Configure and bind the EmailSettings section from the configuration.
        services.Configure<EmailSettings>(configuration.GetSection(nameof(EmailSettings)));

        // Add a transient dependency for IEmailService with the implementation of EmailService.
        services.AddTransient<IEmailService, EmailService>();

        // Add a transient for IHttpContextAccessorService with implimentation of HttpContextAccessorService.
        services.AddTransient<IHttpContextAccessorService, HttpContextAccessorService>();

        // Configure and bind the LogsDatabaseSettings section from the configuration.
        services.Configure<LogsDatabaseSettings>(configuration.GetSection(nameof(LogsDatabaseSettings)));

        // Add a scoped dependency for LogService.
        services.AddScoped<LogService>();

        // Add a scoped dependency for ILogRepository with the implementation of LogRepository.
        services.AddScoped<IPatientExaminationCardRepository, PatientExaminationCardRepository>();

        // Add a scoped dependency for ISuperviseRepository with the implementation of SuperviseRepository.
        services.AddScoped<ISuperviseRepository, SuperviseRepository>();

        return services;
    }
}

