using App.Application.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text;

namespace App.Infrastructure.GeneratePassword;

public class GeneratePassword : IGeneratePassword
{
    private readonly IOptions<IdentityOptions> _opts;

    public GeneratePassword(IOptions<IdentityOptions> opts) => _opts = opts;

    /// <summary>
    /// Generates a random password based on the IdentityOptions.
    /// </summary>
    /// <param name="opts">The IdentityOptions.</param>
    /// <returns>The randomly generated password.</returns>
    public string GenerateRandomPassword()
    {
        var opt = _opts.Value;
        var allowedChars = new StringBuilder();

        if (opt.Password.RequireDigit)
            allowedChars.Append("0123456789");

        if (opt.Password.RequireLowercase)
            allowedChars.Append("abcdefghijklmnopqrstuvwxyz");

        if (opt.Password.RequireUppercase)
            allowedChars.Append("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        if (opt.Password.RequireNonAlphanumeric)
            allowedChars.Append("!@#$%^&*()-_=+[]{}<>:;,.?");

        var random = new Random();
        var passwordBuilder = new StringBuilder();

        for (int i = 0; i < opt.Password.RequiredLength; i++)
        {
            var randomChar = allowedChars[random.Next(allowedChars.Length)];
            passwordBuilder.Append(randomChar);
        }

        var password = passwordBuilder.ToString();

        if (opt.Password.RequireDigit && !password.Any(char.IsDigit))
            password = ReplaceRandomCharacter(password, random, "0123456789");

        if (opt.Password.RequireLowercase && !password.Any(char.IsLower))
            password = ReplaceRandomCharacter(password, random, "abcdefghijklmnopqrstuvwxyz");

        if (opt.Password.RequireUppercase && !password.Any(char.IsUpper))
            password = ReplaceRandomCharacter(password, random, "ABCDEFGHIJKLMNOPQRSTUVWXYZ");

        if (opt.Password.RequireNonAlphanumeric && !password.Any(IsNonAlphanumeric))
            password = ReplaceRandomCharacter(password, random, "!@#$%^&*()-_=+[]{}<>:;,.?");

        return password;
    }

    /// <summary>
    /// Replaces a random character in the password with a character from the specified character pool.
    /// </summary>
    /// <param name="password">The password string.</param>
    /// <param name="random">The Random instance.</param>
    /// <param name="charPool">The character pool to choose from.</param>
    /// <returns>The password with a randomly replaced character.</returns>
    private static string ReplaceRandomCharacter(string password, Random random, string charPool)
    {
        var randomIndex = random.Next(password.Length);
        var randomChar = charPool[random.Next(charPool.Length)];
        return password.Remove(randomIndex, 1).Insert(randomIndex, randomChar.ToString());
    }

    /// <summary>
    /// Checks if the provided character is non-alphanumeric.
    /// </summary>
    /// <param name="c">The character.</param>
    /// <returns>True if the character is non-alphanumeric, false otherwise.</returns>
    private static bool IsNonAlphanumeric(char c)
    {
        return !char.IsLetterOrDigit(c);
    }
}
