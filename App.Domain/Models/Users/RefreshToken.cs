namespace App.Domain.Models.Users
{
    /// <summary>
    /// Represents a Refresh Token entity used for managing user authentication tokens.
    /// </summary>
    public class RefreshToken
    {
        /// <summary>
        /// Gets or sets the unique identifier for the Refresh Token.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the user ID associated with this Refresh Token.
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets the User object associated with this Refresh Token.
        /// </summary>
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// Gets or sets the actual Refresh Token value.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the expiration date and time for the Refresh Token.
        /// The default value is set to one week (7 days) from the current UTC time.
        /// </summary>
        public DateTime Expires { get; set; } = DateTime.UtcNow.AddDays(7);

        /// <summary>
        /// Gets a value indicating whether the Refresh Token has expired.
        /// </summary>
        public bool IsExpired => DateTime.UtcNow >= Expires;

        /// <summary>
        /// Gets or sets the date and time when the Refresh Token was revoked.
        /// This value is null if the token is active and has not been revoked.
        /// </summary>
        public DateTime? Revoked { get; set; }

        /// <summary>
        /// Gets a value indicating whether the Refresh Token is currently active.
        /// It checks if the token has not been revoked and if it is not expired.
        /// </summary>
        public bool IsActive => Revoked == null && !IsExpired;
    }
}
