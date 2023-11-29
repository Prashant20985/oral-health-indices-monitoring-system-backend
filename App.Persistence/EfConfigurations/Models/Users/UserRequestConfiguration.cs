using App.Domain.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Persistence.EfConfigurations.Models.Users
{
    /// <summary>
    /// Configuration class for the UserRequest entity in the Entity Framework.
    /// </summary>
    internal class UserRequestConfiguration : IEntityTypeConfiguration<UserRequest>
    {
        /// <summary>
        /// Configures the entity properties for the UserRequest model.
        /// </summary>
        /// <param name="builder">The entity type builder for the UserRequest model.</param>
        public void Configure(EntityTypeBuilder<UserRequest> builder)
        {
            // Sets the primary key for the UserRequest entity.
            builder.HasKey(x => x.Id);

            // Configures the RequestTitle property, making it required and limiting its maximum length to 500 characters.
            builder.Property(x => x.RequestTitle)
                .IsRequired()
                .HasMaxLength(500);

            // Configures the Description property, limiting its maximum length to 500 characters.
            builder.Property(x => x.Description)
                .HasMaxLength(500);

            // Configures the AdminComment property, limiting its maximum length to 500 characters.
            builder.Property(x => x.AdminComment)
                .HasMaxLength(500);

            // Configures the RequestStatus property, making it required and limiting its maximum length to 20 characters.
            builder.Property(x => x.RequestStatus)
                .IsRequired()
                .HasMaxLength(20);

            // Configures the DateSubmitted property, making it required.

            builder.Property(x => x.DateSubmitted)
                .IsRequired();

            // Defines a relationship between UserRequest and ApplicationUser, setting a foreign key relationship and specifying cascade delete behavior.
            builder.HasOne(x => x.ApplicationUser)
                .WithMany(x => x.UserRequests)
                .HasForeignKey(x => x.CreatedBy)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
