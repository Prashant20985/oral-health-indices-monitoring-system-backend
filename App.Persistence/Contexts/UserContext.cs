using App.Domain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Contexts;

public class UserContext : IdentityDbContext<
    ApplicationUser, ApplicationRole, string,
    IdentityUserClaim<string>, ApplicationUserRole,
    IdentityUserLogin<string>, IdentityRoleClaim<string>,
    IdentityUserToken<string>>
{
    /// <summary>
    /// Parameterless constructor provided for the framework to create an instance of the UserContext class.
    /// </summary>
    public UserContext()
    {

    }

    /// <summary>
    /// Constructor that accepts DbContextOptions<UserContext> as a parameter and passes it to the base class constructor.
    /// Allows the configuration options for the UserContext to be provided externally.
    /// </summary>
    /// <param name="options">The options for configuring the UserContext.</param>
    public UserContext(DbContextOptions<UserContext> options) : base(options)
    {

    }

    /// <summary>
    /// Represents the database table for the User entities.
    /// </summary>
    public virtual DbSet<ApplicationUser> ApplicationUsers { get; set; }

    /// <summary>
    /// Represents the database table for the RefreshToken entities.
    /// </summary>
    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    /// <summary>
    /// Represents the database table for the ApplicationRole entities.
    /// </summary>
    public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }

    /// <summary>
    /// Represents the database table for the ApplicationUserRole entities.
    /// </summary>
    public virtual DbSet<ApplicationUserRole> ApplicationUserRoles { get; set; }

    /// <summary>
    /// Represents the database table for the Group entities.
    /// </summary>
    public virtual DbSet<Group> Groups { get; set; }

    /// <summary>
    /// Represents the database table for the StudentGroup entities.
    /// </summary>
    public virtual DbSet<StudentGroup> StudentGroups { get; set; }

    /// <summary>
    /// Overrides the OnModelCreating method from the base class to provide custom model configuration.
    /// </summary>
    /// <param name="modelBuilder">The model builder instance to configure the model.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Sets the default schema for the model to "user".
        modelBuilder.HasDefaultSchema("user");

        modelBuilder.Entity<ApplicationRole>().ToTable(nameof(ApplicationRole));

        modelBuilder.Entity<ApplicationUserRole>().ToTable(nameof(ApplicationUserRole));

        modelBuilder.Entity<ApplicationUser>().ToTable(nameof(ApplicationUser));

        modelBuilder.Entity<RefreshToken>().ToTable(nameof(RefreshToken));

        modelBuilder.Entity<Group>().ToTable(nameof(Group));

        modelBuilder.Entity<StudentGroup>().ToTable(nameof(StudentGroup));

        // Ignores the IdentityUserClaim, IdentityRoleClaim, IdentityUserLogin, and IdentityUserToken entities.
        modelBuilder.Ignore<IdentityUserClaim<string>>();
        modelBuilder.Ignore<IdentityRoleClaim<string>>();
        modelBuilder.Ignore<IdentityUserLogin<string>>();
        modelBuilder.Ignore<IdentityUserToken<string>>();

        // Applies the entity configurations defined in the assembly containing the UserContext class.
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserContext).Assembly);
    }
}
