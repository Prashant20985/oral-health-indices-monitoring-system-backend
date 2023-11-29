using System.Reflection;

namespace App.Application;

/// <summary>
/// A utility class to reference the assembly containing the App.Application code.
/// </summary>
public static class AssemblyReference
{
    /// <summary>
    /// Gets the <see cref="Assembly"/> object representing the assembly containing the App.Application code.
    /// </summary>
    public static readonly Assembly assembly = typeof(Assembly).Assembly;
}